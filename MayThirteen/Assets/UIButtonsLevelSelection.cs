using UnityEngine;
using System.Collections;

public class UIButtonsLevelSelection : MonoBehaviour
{
		GameObject homeButton;
		GameObject muteButton;
		// Use this for initialization
		void Start ()
		{
				muteButton = GameObject.Find ("MuteButton");
				muteButton.SetActive (false);
				homeButton = GameObject.Find ("HomeButton");
				homeButton.SetActive (false);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public void showPausedButtons ()
		{
				muteButton.SetActive (true);
				homeButton.SetActive (true);
		}
		public void hidePausedButtons ()
		{
				muteButton.SetActive (false);
				homeButton.SetActive (false);
		}
}
