using UnityEngine;
using System.Collections;

public class BuyNoAdsButton : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				if (PlayerPrefs.GetInt ("NoAds", 0) != 1) {
						gameObject.SetActive (true);
				} else {
						gameObject.SetActive (false);
				}
				#if UNITY_EDITOR
				gameObject.SetActive (true);
				#endif
		}
}
