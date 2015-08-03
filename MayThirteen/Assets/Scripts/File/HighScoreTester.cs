using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreTester : MonoBehaviour
{

		public Highscores highscores;

		public int saveLevel = 1;
		public float saveTime;
		public Text singleLevelText;
		public Text highscoreText;
		void Start ()
		{
	
		}
		
		public void SaveTime ()
		{
				highscores.SaveHighScoreAtLevel (saveLevel, saveTime);
				print ("saveTime " + saveTime);
		}
		public void LoadTime ()
		{
				float time = highscores.LoadHighscoreAtLevel (saveLevel);
				singleLevelText.text = saveLevel + ".\t" + time;
		}
		public void LoadTimeForList ()
		{
				float[] highscore = highscores.LoadHighscores ();
				if (highscore != null) {
						UpdateList (highscore);
				}
		}
		public void ResetData ()
		{
				highscores.SaveNew ();
		}
		
		public void UpdateList (float[] highscores)
		{
				string list = "Highscore\n";
				for (int i = 0; i < highscores.Length; i++) {
						if (highscores [i] == float.MaxValue) {
								list += (i + 1) + ".\t" + "\n";
						} else {
								list += (i + 1) + ".\t" + highscores [i] + "\n";
						}
				}
				highscoreText.text = list;
		}
		// Update is called once per frame
		
}
