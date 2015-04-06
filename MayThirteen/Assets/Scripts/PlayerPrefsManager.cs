using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{
		private const string IS_SOUND_ON = "SoundOn";

		private const string LEVELS_DONE = "LevelsDone";
		private const string CURRENT_LEVEL = "CurrentLevel";
		private const string DONE_FIRST_LEVEL = "DoneFirstLevel";
		


		private const string STARS = "Stars";

		private const string NO_ADS = "NoAds";

		private const string STONE_POS_X = "PlayerXPos";
		private const string STONE_POS_Y = "PlayerYPos";

		/*
		 * Sound
		 */
		public static void SetSoundOff ()
		{
				PlayerPrefs.SetInt (IS_SOUND_ON, 0);
				
		}
		public static void SetSoundOn ()
		{
				PlayerPrefs.SetInt (IS_SOUND_ON, 1);
				
		}
		public static bool IsSoundOn ()
		{	
				
				return PlayerPrefs.GetInt (IS_SOUND_ON, 1) == 1 ? true : false;
		}
		/*
	 * Levels
	 */
		public static int GetLevelsDone ()
		{
				return PlayerPrefs.GetInt (LEVELS_DONE);
		}
		public static int GetCurrentLevel ()
		{
				return PlayerPrefs.GetInt (CURRENT_LEVEL, 0);
		}
		public static void SetLevelsDone (int num)
		{
				PlayerPrefs.SetInt (LEVELS_DONE, num);
		}
		public static void SetCurrentLevel (int num)
		{
				PlayerPrefs.SetInt (CURRENT_LEVEL, num);
		}

		public static void SetDoneFirstLevel (bool doneFirst)
		{
				int x = doneFirst ? 1 : 0;
				PlayerPrefs.SetInt (DONE_FIRST_LEVEL, x);
		}
		public static bool DoneFirstLevel ()
		{
				return PlayerPrefs.GetInt (DONE_FIRST_LEVEL) == 1 ? true : false;
		}
		/*
		 * STARS
	 	*/
		public static int GetStars ()
		{
				return PlayerPrefs.GetInt (STARS, 0);
		}
		public static void SetStars (int num)
		{
				PlayerPrefs.SetInt (STARS, num);
		}
		/*
		 * ADS
	 	*/
		public static void RemoveAds ()
		{
				PlayerPrefs.SetInt (NO_ADS, 1);
		}
		public static void ShowAds ()
		{
				PlayerPrefs.SetInt (NO_ADS, 0);
		}
		public static bool AdsEnabled ()
		{
				return PlayerPrefs.GetInt (NO_ADS, 0) == 0 ? true : false;
		}
		

		/*
		* Stone
	 	*/
		
		public static void SetStonePosition (float x, float y)
		{
				PlayerPrefs.SetFloat (STONE_POS_X, x);
				PlayerPrefs.SetFloat (STONE_POS_Y, y);
		}
		public static bool HasStoneKey ()
		{
				if (PlayerPrefs.HasKey (STONE_POS_X) && PlayerPrefs.HasKey (STONE_POS_Y)) {
						return true;
				} else {
						return false;
				}
		}
		public static float GetStonePositionX ()
		{
				return 	PlayerPrefs.GetFloat (STONE_POS_X);
		}
		public static float GetStonePositionY ()
		{
				return PlayerPrefs.GetFloat (STONE_POS_Y);
			
		}
}
