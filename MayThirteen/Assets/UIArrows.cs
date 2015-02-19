using UnityEngine;
using System.Collections;

public class UIArrows : MonoBehaviour
{



		public void HideArrows ()
		{
				
				foreach (Transform child in transform) {
						child.gameObject.SetActive (false);
				}
		}
		public void ShowArrows ()
		{
				foreach (Transform child in transform) {
						child.gameObject.SetActive (true);
				}
		}
}
