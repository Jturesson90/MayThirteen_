using UnityEngine;
using System.Collections;

public class MenuUIHelper : MonoBehaviour
{
		private GameObject mainMenuUI;
		float timer = 0f;
		public float showGuiAfter = 2f;
		private bool showedGUI = false;
		private LevelSwitcher levelSwitcher;
	
		void Awake ()
		{
				levelSwitcher = GameObject.Find ("LevelSwitcher").GetComponent<LevelSwitcher> ();
				
				PlayerPrefsManager.SetDoneFirstLevel (true);
				mainMenuUI = GameObject.Find ("MainMenuUI");
		
				hideGUI ();
		}
		void Start ()
		{
				Time.timeScale = 1f;
		}
		void Update ()
		{
				timer += Time.deltaTime;
				if (timer > showGuiAfter && !showedGUI) {
						showGUI ();
						showedGUI = true;
				}
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Application.Quit ();	
			
				}
		
		}
	
		void hideGUI ()
		{
				mainMenuUI.SetActive (false);
		}
	
		void showGUI ()
		{
				mainMenuUI.SetActive (true);
		
		}
		public void BuyNoAds ()
		{
		
		}
		public void StartGame ()
		{
		
				if (!PlayerPrefsManager.DoneFirstLevel ()) {			
						LoadLevel ("LevelX");
				} else {
			
						LoadLevel ("LevelSelectionLobby");
				}
		}
		private void LoadLevel (string level)
		{
				levelSwitcher.SwitchLevel (level);
		
		
		
		}
}
