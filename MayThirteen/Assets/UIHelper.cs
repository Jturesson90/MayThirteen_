using UnityEngine;
using System.Collections;

public class UIHelper : MonoBehaviour
{
		private UIArrows uiArrows;
		private PauseButton pauseButton;
		private UIStars uiStars;
		private UIButtonsLevelSelection uiButtons;
		// Use this for initialization

		void Awake ()
		{
				uiButtons = GameObject.Find ("UIButtons").GetComponent<UIButtonsLevelSelection> ();
				uiStars = GameObject.Find ("UIStars").GetComponent<UIStars> ();
				pauseButton = GameObject.Find ("PauseButton").GetComponent<PauseButton> ();
				uiArrows = GameObject.Find ("UIArrows").GetComponent<UIArrows> ();
		}
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						if (Application.loadedLevelName == "LevelSelectionLobby") {
								TogglePause ();
						} else if (Application.loadedLevelName == "Menu") {
								Application.Quit ();	
						} else {
								TogglePause ();
						}
				}
		}

		
	
		public void TogglePause ()
		{

				if (Time.timeScale == 0.0f) {
						Time.timeScale = 1.0f;
						uiArrows.ShowArrows ();
						uiStars.HideStars ();
						uiButtons.hidePausedButtons ();
				} else {
						Time.timeScale = 0.0f;
						uiArrows.HideArrows ();
						uiStars.ShowStars ();
						uiButtons.showPausedButtons ();
						
				}
				pauseButton.TogglePause ();
		}
		public void Pause ()
		{
				Time.timeScale = 0.0f;
				uiArrows.HideArrows ();
		}

		public void UnPause ()
		{
				Time.timeScale = 1.0f;
				
		}
		public void LoadLevel (string level)
		{
				Application.LoadLevel (level);
		}

}
