using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIHelper : MonoBehaviour
{
		private GameObject uiArrows;

		private GameObject pausedItems;
		private GameObject gameItems;
		private GameObject allAroundItems;
		private GameObject wonItems;
		private GameObject tryAgainItems;

		private GameObject pauseArrow;

		private bool canExit = true;
		
		
		void Awake ()
		{
				wonItems = GameObject.Find ("WonItems");
				wonItems.SetActive (false);

				allAroundItems = GameObject.Find ("AllAroundItems");
				pausedItems = GameObject.Find ("PausedItems");
				gameItems = GameObject.Find ("GameItems");
				tryAgainItems = GameObject.Find ("TryAgainItems");

				uiArrows = GameObject.Find ("UIArrows");
				pauseArrow = GameObject.Find ("PauseArrow");
	
				HideUIOnStart ();
		
		}
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape) && canExit) {
						TogglePause ();
				}
		}

		public void TogglePause ()
		{
				if (Time.timeScale == 0.0f) {
						Resume ();
			
				} else {
						Pause ();
				
				}

		}
		private void Pause ()
		{
				Time.timeScale = 0.0f;

				gameItems.SetActive (false);
				pausedItems.SetActive (true);
				SetPauseLevelText ("Level " + PlayerPrefsManager.GetCurrentLevel ());
				pauseArrow.SetActive (true);
		}
		private void HideUIOnStart ()
		{
				HideArrows ();
				tryAgainItems.SetActive (false);
				pausedItems.SetActive (false);
				gameItems.SetActive (true);
		}
		private void Resume ()
		{
				Time.timeScale = 1.0f;

				gameItems.SetActive (true);
				pausedItems.SetActive (false);

				pauseArrow.SetActive (false);
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
		
		public void Win ()
		{
				canExit = false;
				allAroundItems.SetActive (false);
				pausedItems.SetActive (false);
				gameItems.SetActive (false);
				StartCoroutine (ShowWonItems ());

		}
		private IEnumerator ShowWonItems ()
		{
				yield return new WaitForSeconds (1f);
				wonItems.SetActive (true);
		}
		private void SetPauseLevelText (string text)
		{
				GameObject.Find ("PausedLevelText").GetComponent<Text> ().text = text;
				
		}

		public void Lost ()
		{
				canExit = false;
				allAroundItems.SetActive (false);
				pausedItems.SetActive (false);
				gameItems.SetActive (false);
				StartCoroutine (ShowTryAgainItems ());
		}
		private IEnumerator ShowTryAgainItems ()
		{
				yield return new WaitForSeconds (1f);
				tryAgainItems.SetActive (true);
		}
}
