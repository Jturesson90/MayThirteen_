using UnityEngine;
using System.Collections;

public class UIHelper : MonoBehaviour
{
		private GameObject uiArrows;
		private PauseButton pauseButton;
		private UIStars uiStars;
		private UIButtonsLevelSelection uiButtons;
		private UIEnterLevelText uiEnterLevelText;

		void HideUI ()
		{
				HideArrows ();
		}
		
		// Use this for initialization

		void Awake ()
		{
				FetchGameObjects ();
				FetchScripts ();
				HideUI ();
		}
		void FetchGameObjects ()
		{
				uiArrows = GameObject.Find ("UIArrows");
		}
		void FetchScripts ()
		{
				uiButtons = GameObject.Find ("UIButtons").GetComponent<UIButtonsLevelSelection> ();
				uiStars = GameObject.Find ("UIStars").GetComponent<UIStars> ();
				pauseButton = GameObject.Find ("PauseButton").GetComponent<PauseButton> ();
				uiEnterLevelText = GameObject.Find ("UIEnterLevelText").GetComponent<UIEnterLevelText> ();
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						TogglePause ();
				}
		}

		
		private bool isArrowsActive = false;
		public void TogglePause ()
		{

				if (Time.timeScale == 0.0f) {
						Time.timeScale = 1.0f;
						uiStars.HideStars ();
						uiButtons.hidePausedButtons ();
						if (isArrowsActive) {
								ShowArrows ();
								isArrowsActive = false;
						}
			
				} else {
						Time.timeScale = 0.0f;
						uiStars.ShowStars ();
						uiButtons.showPausedButtons ();
						isArrowsActive = uiArrows.activeSelf;
						if (isArrowsActive) {
								HideArrows ();
						}

			
				}
				pauseButton.TogglePause ();
		}
		public void Pause ()
		{
				Time.timeScale = 0.0f;
		}

		public void UnPause ()
		{
				Time.timeScale = 1.0f;
				
		}
		public void LoadLevel (string level)
		{
				Application.LoadLevel (level);
		}
		
		public void ShowLevelText (int selectedLevel)
		{
				string text = "Click to enter level " + selectedLevel + "!";
				
				uiEnterLevelText.setText (text);
		}
		public void HideLevelText ()
		{
				uiEnterLevelText.setText ("");
		}
		public void ShowArrows ()
		{
				if (uiArrows != null) {
						uiArrows.SetActive (true);
				}
		}
		public void HideArrows ()
		{
				if (uiArrows != null) {
						uiArrows.SetActive (false);
				}
		}

}
