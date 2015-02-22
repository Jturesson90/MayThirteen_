using UnityEngine;
using System.Collections;

public class UIButtonsLevelSelection : MonoBehaviour
{
		GameObject homeButton;
		GameObject muteButton;
		GameObject adButton;
		// Use this for initialization
		void Start ()
		{
				adButton = GameObject.Find ("BuyNoAdsHolder");
				adButton.SetActive (false);
				muteButton = GameObject.Find ("MuteButton");
				muteButton.SetActive (false);
				homeButton = GameObject.Find ("HomeButton");
				homeButton.SetActive (false);
		}

		public void showPausedButtons ()
		{
				muteButton.SetActive (true);
				homeButton.SetActive (true);
				adButton.SetActive (true);
		}
		public void hidePausedButtons ()
		{
				adButton.SetActive (false);
				muteButton.SetActive (false);
				homeButton.SetActive (false);
		}
}
