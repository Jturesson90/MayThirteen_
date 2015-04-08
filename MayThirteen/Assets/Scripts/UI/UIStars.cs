using UnityEngine;
using System.Collections;

public class UIStars : MonoBehaviour
{
		void Start ()
		{
				HideStars ();
		}
		public void HideStars ()
		{
		
				foreach (Transform child in transform) {
						child.gameObject.SetActive (false);
				}
		}
		public void ShowStars ()
		{
				foreach (Transform child in transform) {
						child.gameObject.SetActive (true);
				}
		}
}
