using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
		private GameObject uiArrows;
		private GameObject uiLevelText;

		private PauseButton pauseButton;
		private UIStars uiStars;
		private UIButtonsLevelSelection uiButtons;
		private UIEnterLevelText uiEnterLevelText;
		
		
		private bool isArrowsActive = false;

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
				uiLevelText = GameObject.Find ("UIEnterLevelText");
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

		
		
		public void TogglePause ()
		{

				if (Time.timeScale == 0.0f) {
						Time.timeScale = 1.0f;
						uiStars.HideStars ();
						uiButtons.hidePausedButtons ();
						uiLevelText.SetActive (true);
						if (isArrowsActive) {
								ShowArrows ();
								isArrowsActive = false;
						}
			
				} else {
						Time.timeScale = 0.0f;
						uiStars.ShowStars ();
						uiButtons.showPausedButtons ();
						uiLevelText.SetActive (false);
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
		
		public void ShowLevelText (int selectedLevel)
		{
				string text = "";
#if UNITY_IPHONE || UNITY_ANDROID
				text = "Click to enter level " + selectedLevel + "!";
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
				text = "Press Space to enter level " + selectedLevel + "!";
#endif

				
				
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
