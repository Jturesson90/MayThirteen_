using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimeView : MonoBehaviour
{
		GameObject superStar;
		Text text;
		Text winText;
		Text highscoreText;
		private bool shouldCount = false;
		private float time = 0f;
		float starTime = 1000f;		

		private GameObject starImage;

		void Awake ()
		{
				superStar = GameObject.Find ("SuperStar");
				superStar.SetActive (false);
				starImage = GameObject.Find ("Star");
				starImage.SetActive (false);
				starTime = GetStarTime ();
				winText = GameObject.Find ("TimerPlankText").GetComponent<Text> ();
				winText.text = "";
				highscoreText = GameObject.Find ("HighscoreText").GetComponent<Text> ();
				highscoreText.text = "";
				text = GetComponent<Text> ();

		}
		public void ShowHighScore (float time)
		{
				
				highscoreText.text = SecondsToMinutes (time) + "  (Best)"; 
		}
		public void newTime (float time)
		{
				text.text = SecondsToMinutes (time);
		}
		
		public void StartTimer ()
		{
				shouldCount = true;
				time = 0f;
				StartCoroutine (UpdateTime ());
		}
		public bool EndTimerAndDidBeatStarTime ()
		{
				shouldCount = false;
				bool didBeatStartTime = CheckStarCondition ();
				return didBeatStartTime;
		}
		public float GetTime ()
		{
				return time;
		}
		public void Stop ()
		{
				shouldCount = false;
		}
		private IEnumerator UpdateTime ()
		{
				while (shouldCount) {
						time += Time.deltaTime;
						newTime (time);
						yield return new WaitForFixedUpdate ();
				}
		}
		private bool CheckStarCondition ()
		{
				if (starTime > time) {

						//	winText.text = "You got a superstar!";
						starImage.SetActive (true);
						return true;
				} else {
						superStar.SetActive (true);
						winText.text = SecondsToMinutes (starTime);
						return false;
				}
		}

		private float GetStarTime ()
		{
				float[] starTimes = new float[20];
				//Level 1-5
				starTimes [0] = 7f;
				starTimes [1] = 12.50f;
				starTimes [2] = 27f;
				starTimes [3] = 31.20f;
				starTimes [4] = 16f;
				//Level 6-10
				starTimes [5] = 8f;
				starTimes [6] = 20f;
				starTimes [7] = 10.80f;
				starTimes [8] = 9f;
				starTimes [9] = 37f;
				//Level 11- 15
				starTimes [10] = 28f;
				starTimes [11] = 46.5f;
				starTimes [12] = 7.2f;
				starTimes [13] = 24f;
				starTimes [14] = 30.30f;
				//Level  16-20
				starTimes [15] = 28f;
				starTimes [16] = 13f;
				starTimes [17] = 2f;
				starTimes [18] = 2f;
				starTimes [19] = 2f;
				
				int currentLevel = PlayerPrefsManager.GetCurrentLevel ();
				return starTimes [currentLevel - 1];
		}

		private string SecondsToMinutes (float time)
		{		
			
				return string.Format ("{0:0}:{1:00}.{2:00}",
		                      Mathf.Floor (time / 60),
		                      Mathf.Floor (time) % 60,
		                      Mathf.Floor ((time * 100) % 100));
				
		}


		
}
