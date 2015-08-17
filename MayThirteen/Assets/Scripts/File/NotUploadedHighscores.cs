using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotUploadedHighscores
{

		private string SAVE_PATH;
		
		public NotUploadedHighscores ()
		{
				SAVE_PATH = Application.persistentDataPath + "/data_losthighscore.dat";
				LostHighscores lostHighscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as LostHighscores;

				if (lostHighscores != null) {
				} else {
						SaveNew ();
				}

		}
		public long[] GetLostHighscores ()
		{
				LostHighscores lostHighscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as LostHighscores;
				return lostHighscores.highscores;
		}
		public void SaveHighScoreAtLevel (int level, long time)
		{
				level--;
				var lostHighscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as LostHighscores;
				if (lostHighscores != null) {
						if (lostHighscores.highscores [level] > time) {
								lostHighscores.highscores [level] = time;
								EasySerializer.SerializeObjectToFile (lostHighscores, SAVE_PATH);
						} else {
			
						}
			
				} else {
						SaveNew ();
				}
		
		}
		public void ResetScoreAt (int level)
		{
				level--;
				var lostHighscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as LostHighscores;
				if (lostHighscores == null)
						return;
				lostHighscores.highscores [level] = long.MaxValue;
				EasySerializer.SerializeObjectToFile (lostHighscores, SAVE_PATH);
		}
		public void ResetLostHighscore ()
		{
				var lostHighscores = EasySerializer.DeserializeObjectFromFile (SAVE_PATH) as LostHighscores;
				if (lostHighscores == null)
						return;
				for (int i = 0; i < lostHighscores.highscores.Length; i++) {
						lostHighscores.highscores [i] = long.MaxValue;
				}
				EasySerializer.SerializeObjectToFile (lostHighscores, SAVE_PATH);
				
		}
		
		
		public void SaveNew ()
		{
				LostHighscores lostHighscores = new LostHighscores ();
				EasySerializer.SerializeObjectToFile (lostHighscores, SAVE_PATH);
		
		}
		
		
}
[System.Serializable]
class LostHighscores
{
		public long[] highscores;
		public LostHighscores ()
		{	
				highscores = new long[LevelHandlerC.NUM_OF_LEVELS];
		
				for (int i = 0; i < highscores.Length; i++) {
						highscores [i] = long.MaxValue;
				}
		}
}
