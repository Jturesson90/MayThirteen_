using UnityEngine;
using System.Collections;

public class GuiHandlerMenu : MonoBehaviour
{
		private FadeInScript fade;
		private GameObject mainMenuCanvas;
		float timer = 0f;
		public float showGuiAfter = 2f;
		private bool showedGUI = false;

		void Awake ()
		{
				mainMenuCanvas = GameObject.Find ("MainMenuCanvas");
				fade = GameObject.Find ("FadeInObject").GetComponent ("FadeInScript") as FadeInScript;

				hideGUI ();
		}
		void Update ()
		{
				timer += Time.deltaTime;
				if (timer > showGuiAfter && !showedGUI) {
						showGUI ();
						showedGUI = true;
				}
				
		}

		void hideGUI ()
		{
				mainMenuCanvas.SetActive (false);
		}

		void showGUI ()
		{
				mainMenuCanvas.SetActive (true);
	
		}
		public void BuyNoAds ()
		{

		}
		public void StartGame ()
		{
				if (PlayerPrefs.GetInt ("DoneFirstLevel", 0) != 1) {
						hideGUI ();
						fade.FadeToLevel ("LevelX");
				
						
				} else {
						hideGUI ();
						fade.FadeToLevel ("LevelSelectionLobby");
						
				}
		}
}
