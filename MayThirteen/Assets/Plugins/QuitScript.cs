using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour
{
		public float boxHeight = 50;
		public float boxWidth = 50;
		public float boxX = 50;
		public float boxY = 50;
		public Texture quit;
		private bool showGUI = false;
		private float width = 1920.0f;
		private float height = 1080.0f;
		public GUISkin NoSkin;
		public GUISkin YesSkin;
		private float buttonSize = 200.0f;
		public float buttonY;
		public float buttonX;
		public static bool isShowingQuit = false;
		
		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (Input.GetKeyDown (KeyCode.Escape)) {
						if (Application.loadedLevelName == "Splash")
								return;
						if (Application.loadedLevelName == "LevelSelectionLobby") {
								Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
						} else if (Application.loadedLevelName == "Menu") {
								Application.Quit ();	
								//showGUI = showGUI ? false : true;
						} else {
								Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
						}
				}
		}

		void OnGUI ()
		{
				GUI.matrix = Matrix4x4.TRS (new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity, new Vector3 (Screen.width / 1920.0f, Screen.height / 1080.0f, 1.0f)); 
				if (!showGUI) {
						isShowingQuit = false;
						return;
						
				}
				isShowingQuit = true;
				GUI.DrawTexture (new Rect (width / 2 - quit.width / 2, height / 2 - quit.height / 2, quit.width, quit.height), quit);
				GUI.skin = NoSkin;
				if (GUI.Button (new Rect (width / 2 + buttonX - buttonSize / 2, buttonY, buttonSize, buttonSize), "")) {
						Time.timeScale = 1.0f;
						showGUI = false;
				}
				GUI.skin = YesSkin;
				if (GUI.Button (new Rect (width / 2 - buttonX - buttonSize / 2, buttonY, buttonSize, buttonSize), "")) {
						Application.Quit ();
				}
		}
}
