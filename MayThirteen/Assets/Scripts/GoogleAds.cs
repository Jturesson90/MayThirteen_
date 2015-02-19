using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
		
		public static GoogleAds googleAds;
		public string AndroidBannerID = "";
		public string AndroidPageID = "";
		public string IOSBannerID = "";
		public string savedSceneName = "";
		public GUIStyle myFont;
		public string IOSPageID = "";
		private BannerView bannerView;
		private InterstitialAd interstitial;
		private string myID = "";
		private AdRequest bigRequest;
		private bool shouldShowAds = true;
		private float timer = 6f;

		void Awake ()
		{
	
				if (googleAds == null) {
						DontDestroyOnLoad (this.gameObject);
						googleAds = this;
				} else if (googleAds != this) {
						Destroy (gameObject);
				}
		}

		void Start ()
		{
				shouldShowAds = PlayerPrefs.GetInt ("NoAds", 0) != 1 ? true : false;


				
				//DebugAdds ();
				ReleaseAdds ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				timer += Time.deltaTime;
				if (timer > 5f) {
						shouldShowAds = PlayerPrefs.GetInt ("NoAds", 0) != 1 ? true : false;
						timer = 0f;
				}
				if (bannerView != null) {
						if (shouldShowAds) {
								bannerView.Show ();	
						
						} else {
								bannerView.Hide ();		
						}
				}
				if (savedSceneName != Application.loadedLevelName) {
						savedSceneName = Application.loadedLevelName;
						LoadPageAd ();
				}
		}

		void DebugAdds ()
		{
#if UNITY_ANDROID	
				if (Application.platform == RuntimePlatform.Android) {
						AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
						AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");  
						AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
						myID = secure.CallStatic<string> ("getString", contentResolver, "android_id");		
						
						bannerView = new BannerView (AndroidBannerID, AdSize.Banner, AdPosition.Bottom);
						AdRequest request = new AdRequest.Builder ().AddTestDevice (myID).Build ();
						bannerView.LoadAd (request);
						

						Debug.Log (myID);
						// Initialize an InterstitialAd.
						interstitial = new InterstitialAd (AndroidPageID);
						// Create an empty ad request.
						bigRequest = new AdRequest.Builder ().AddTestDevice (myID).Build ();
						// Load the interstitial with the request.
						interstitial.LoadAd (bigRequest);
				}
#endif
		}
		
		void ReleaseAdds ()
		{
				string bannerID = "";
				string pageAdID = "";
#if UNITY_ANDROID
				bannerID = AndroidBannerID;
				pageAdID = AndroidPageID;
#elif UNITY_IPHONE
				bannerID = IOSBannerID;
				pageAdID = IOSPageID;
#endif
				bannerView = new BannerView (bannerID, AdSize.Banner, AdPosition.Bottom);
				#if UNITY_ANDROID
				AdRequest request = new AdRequest.Builder ().AddTestDevice ("5c40728084cb5806").Build ();
				#elif UNITY_IPHONE
				AdRequest request = new AdRequest.Builder ().Build ();
				#endif
				bannerView.LoadAd (request);
				// Initialize an InterstitialAd.
				interstitial = new InterstitialAd (pageAdID);
				// Create an empty ad request.
				#if UNITY_ANDROID
				bigRequest = new AdRequest.Builder ().AddTestDevice ("5c40728084cb5806").Build ();
				#elif UNITY_IPHONE
				bigRequest = new AdRequest.Builder ().Build ();
				#endif
				// Load the interstitial with the request.
				interstitial.LoadAd (bigRequest);
				
		}
	
		void OnApplicationQuit ()
		{
				if (bannerView != null) {
						bannerView.Destroy ();
				}
				if (interstitial != null) {
						interstitial.Destroy ();		
				}
		}

		public void LoadPageAd ()
		{
				print ("LOADPAGE");
				if (shouldShowAds) {
						if (!PlayerPrefs.HasKey ("ads"))
								PlayerPrefs.SetInt ("ads", 0);
						if (PlayerPrefs.GetInt ("ads", 0) < 0) {
								PlayerPrefs.SetInt ("ads", 1);
						}
						if (interstitial != null) {
								if (Application.loadedLevelName == "LevelSelectionLobby" && interstitial.IsLoaded () && PlayerPrefs.GetInt ("ads", 0) >= 1) {
										interstitial.Show ();
										PlayerPrefs.SetInt ("ads", 0);
								} else if (Application.loadedLevelName == "LevelSelectionLobby" && PlayerPrefs.GetInt ("ads", 0) > 0) {
										interstitial.LoadAd (bigRequest);
										PlayerPrefs.SetInt ("ads", 2);
								} else {
										PlayerPrefs.SetInt ("ads", 1);
								}
						}
				}
		}
}
