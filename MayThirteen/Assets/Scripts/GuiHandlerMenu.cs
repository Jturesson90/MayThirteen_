using UnityEngine;
using System.Collections;

public class GuiHandlerMenu : MonoBehaviour
{
		float timer = 0f;
		void Awake ()
		{
				hideGUI ();
		}
		void Update ()
		{
				timer += Time.deltaTime;
				if (timer > 2f) {
						showGUI ();
				}
				
		}

		void hideGUI ()
		{
				foreach (Transform child in gameObject.transform) {
						child.gameObject.SetActive (false);
				}
		}

		void showGUI ()
		{
				foreach (Transform child in gameObject.transform) {
						child.gameObject.SetActive (true);
				}
	
		}
}
