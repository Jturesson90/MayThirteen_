using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimeView : MonoBehaviour
{

		Text text;
		Text winText;
		private bool shouldCount = false;
		private float time = 0f;
		float starTime = 1000f;		

		private GameObject starImage;

		void Awake ()
		{
				starImage = GameObject.Find ("Star");
				starImage.SetActive (false);
				starTime = GetStarTime ();
				winText = GameObject.Find ("TimerPlankText").GetComponent<Text> ();
				winText.text = "";
				text = GetComponent<Text> ();

		}
		public void newTime (float time)
		{
				text.text = secondsToMinutes (time);
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
						winText.text = "You got a superstar!";
						starImage.SetActive (true);
						return true;
				} else {
						winText.text = "Superstar time: " + secondsToMinutes (starTime);
						return false;
				}
		}

		private float GetStarTime ()
		{
				float[] starTimes = new float[20];
				starTimes [0] = 8f;
				starTimes [1] = 12.5f;
				starTimes [2] = 27f;
				starTimes [3] = 22f;
				starTimes [4] = 17f;
				starTimes [5] = 2f;
				starTimes [6] = 2f;
				starTimes [7] = 2f;
				starTimes [8] = 2f;
				starTimes [9] = 2f;
				starTimes [10] = 2f;
				starTimes [11] = 2f;
				starTimes [12] = 2f;
				starTimes [13] = 2f;
				starTimes [14] = 2f;
				starTimes [15] = 2f;
				starTimes [16] = 2f;
				starTimes [17] = 2f;
				starTimes [18] = 2f;
				starTimes [19] = 2f;
				
				int currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 0);
				return starTimes [currentLevel - 1];
		}

		private string secondsToMinutes (float time)
		{		
			
				return string.Format ("{0:0}:{1:00}.{2:00}",
		                      Mathf.Floor (time / 60),
		                      Mathf.Floor (time) % 60,
		                      Mathf.Floor ((time * 100) % 100));
				
		}


		
}
