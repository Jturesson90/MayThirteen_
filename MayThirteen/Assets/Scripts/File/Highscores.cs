using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour
{
		private string SAVE_PATH;
		private string TEST_SAVE_PATH;
		
		// Use this for initialization
		void Awake ()
		{
				SAVE_PATH = Application.persistentDataPath + "/data_highscore.dat";
				TEST_SAVE_PATH = Application.persistentDataPath + "/data_highscore_test.dat";
				PlayerHighscores highscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as PlayerHighscores;
				if (highscores != null) {
				} else {
						SaveNew ();
				}
		}
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		public void SaveNew ()
		{
				PlayerHighscores highscores = new PlayerHighscores ();
				print ("SavingNew");
				EasySerializer.SerializeObjectToFile (highscores, SAVE_PATH);

		}
		public void SaveHighScoreAtLevel (int level, float time)
		{
				level--;
				PlayerHighscores highscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as PlayerHighscores;
				if (highscores != null) {
						if (highscores.highscores [level] > time) {
								print ("Saving new Score @ level " + (level + 1) + "\nNew score: " + time + "\tOld score: " + highscores.highscores [level]);
								highscores.highscores [level] = time;
								EasySerializer.SerializeObjectToFile (highscores, SAVE_PATH);
								
						} else {
								print ("Old score was better @ " + (level + 1) + "\nNew score: " + time + "\nOld score: " + highscores.highscores [level]);
						}
						
				} else {
						SaveNew ();

				}

		}
		public float[] LoadHighscores ()
		{
				PlayerHighscores highscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as PlayerHighscores;
				return highscores.highscores;
		}
		public float LoadHighscoreAtLevel (int level)
		{
				level--;
				PlayerHighscores highscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as PlayerHighscores;
				if (highscores != null) {
						return highscores.highscores [level];
				} else {
						SaveNew ();
				}
				return float.MaxValue;
				
		}
}
[System.Serializable]
class PlayerHighscores
{
		public float[] highscores;
		public PlayerHighscores ()
		{	
				highscores = new float[LevelHandlerC.NUM_OF_LEVELS];

				for (int i = 0; i < highscores.Length; i++) {
						highscores [i] = float.MaxValue;
				}
		
		}
}