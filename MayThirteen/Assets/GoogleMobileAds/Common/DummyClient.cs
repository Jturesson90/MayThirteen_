using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
		internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
		{
				private bool isShowingAd = false;

				public DummyClient (IAdListener listener)
				{
						Debug.Log ("Created DummyClient");
				}

				public void CreateBannerView (string adUnitId, AdSize adSize, AdPosition position)
				{
						Debug.Log ("Dummy CreateBannerView");
				}

				public void LoadAd (AdRequest request)
				{
						Debug.Log ("Dummy LoadAd");
				}

				public void ShowBannerView ()
				{
						if (!isShowingAd) {
								Debug.Log ("Dummy ShowBannerView");
						}
						isShowingAd = true;

				}

				public void HideBannerView ()
				{
						if (isShowingAd) {
								//	Debug.Log ("Dummy HideBannerView");
						}
						isShowingAd = false;


				}

				public void DestroyBannerView ()
				{
						//		Debug.Log ("Dummy DestroyBannerView");
				}

				public void CreateInterstitialAd (string adUnitId)
				{
						//		Debug.Log ("Dummy CreateIntersitialAd");
				}

				public bool IsLoaded ()
				{
						//	Debug.Log ("Dummy IsLoaded");
						return true;
				}

				public void ShowInterstitial ()
				{
						//		Debug.Log ("Dummy ShowInterstitial");
				}

				public void DestroyInterstitial ()
				{
						//	Debug.Log ("Dummy DestroyInterstitial");
				}
		}
}
