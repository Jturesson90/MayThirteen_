using UnityEngine;
using System.Collections;

public class BuyNoAdsButton : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{

				gameObject.SetActive (PlayerPrefsManager.AdsEnabled ());

				#if UNITY_EDITOR
				gameObject.SetActive (true);
				#endif
		}
}
