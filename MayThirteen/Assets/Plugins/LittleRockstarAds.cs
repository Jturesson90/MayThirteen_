using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class LittleRockstarAds : MonoBehaviour
{
		private static readonly string GAME_ADS_ID = "25362";
		public static LittleRockstarAds instance;
		// Use this for initialization
		
		void Awake ()
		{
				if (instance != null && instance != this) {
						Destroy (this.gameObject);
						return;
				} else {
						instance = this;

				}
				DontDestroyOnLoad (this.gameObject);
		}
		void Start ()
		{
				Advertisement.Initialize (GAME_ADS_ID);
		}
		public void ShowAds ()
		{
				bool showAds = PlayerPrefs.GetInt ("NoAds", 0) == 0 ? true : false;
				if (showAds) {
						if (Advertisement.isReady ()) {
								if (Random.value > 0.45f) {
										Advertisement.Show ();
								}
						
						}
				}
		}
}
