using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class LittleRockstarGoogleGame : MonoBehaviour
{

		

		/*
	 	*Leaderboard
	 	*/

		
			

		private const string LEVEL_1 = "CgkIi7vekMweEAIQDA";
		private const string LEVEL_2 = "CgkIi7vekMweEAIQDQ";
		private const string LEVEL_3 = "CgkIi7vekMweEAIQDg";
		private const string LEVEL_4 = "CgkIi7vekMweEAIQDw";
		private const string LEVEL_5 = "CgkIi7vekMweEAIQEA";
		private const string LEVEL_6 = "CgkIi7vekMweEAIQEQ";
		private const string LEVEL_7 = "CgkIi7vekMweEAIQEg";
		private const string LEVEL_8 = "CgkIi7vekMweEAIQEw";
		private const string LEVEL_9 = "CgkIi7vekMweEAIQFA";
		private const string LEVEL_10 = "CgkIi7vekMweEAIQFQ";
		private const string LEVEL_11 = "CgkIi7vekMweEAIQFg";
		private const string LEVEL_12 = "CgkIi7vekMweEAIQFw";
		private const string LEVEL_13 = "CgkIi7vekMweEAIQGA";
		private const string LEVEL_14 = "CgkIi7vekMweEAIQGQ";
		private const string LEVEL_15 = "CgkIi7vekMweEAIQGg";
		private const string LEVEL_16 = "CgkIi7vekMweEAIQGw";
		private const string LEVEL_17 = "CgkIi7vekMweEAIQHA";
		private const string LEVEL_18 = "CgkIi7vekMweEAIQHQ";
		private const string LEVEL_19 = "CgkIi7vekMweEAIQHg";
		private const string LEVEL_20 = "CgkIi7vekMweEAIQHw";

		/*
		*Cleared Achivements
	 	*/
		private const string CLEARED_5 = "CgkIi7vekMweEAIQAg";
		private const string CLEARED_10 = "CgkIi7vekMweEAIQAw";
		private const string CLEARED_15 = "CgkIi7vekMweEAIQBA";
		private const string CLEARED_20 = "CgkIi7vekMweEAIQBQ";


		/*
		*Star Achivements
	 	*/
		private static readonly string STAR_5 = "CgkIi7vekMweEAIQBg";
		private static readonly string STAR_10 = "CgkIi7vekMweEAIQBw";
		private static readonly string STAR_15 = "CgkIi7vekMweEAIQCA";
		private static readonly string STAR_20 = "CgkIi7vekMweEAIQCQ";

	
		public void UpdateAchievemnts (int stars, int levels)
		{
				
				HandleStarUpdateAchievemnts (stars);
				HandleClearedAchievemnts (levels);
		}
		private void HandleStarUpdateAchievemnts (int stars)
		{
				if (stars >= 20) {
						AchievmentUnlocked (STAR_20);
				} else if (stars >= 15) {
						AchievmentUnlocked (STAR_15);
				} else if (stars >= 10) {
						AchievmentUnlocked (STAR_10);
				} else if (stars >= 5) {
						AchievmentUnlocked (STAR_5);
				}
				
		}
		
		private void HandleClearedAchievemnts (int levels)
		{
				if (levels >= 20) {
						AchievmentUnlocked (CLEARED_20);
				} else if (levels >= 15) {
						AchievmentUnlocked (CLEARED_15);
				} else if (levels >= 10) {
						AchievmentUnlocked (CLEARED_10);
				} else if (levels >= 5) {
						AchievmentUnlocked (CLEARED_5);
				}
				
		}
		private void AchievmentUnlocked (string name)
		{
				if (LoggedIn ())
						Social.ReportProgress (name, 100.0f, (bool success) => {
								// handle success or failure
						});
		}
		bool LoggedIn ()
		{
				return Social.localUser.authenticated;
		}
		void Start ()
		{
				

				GooglePlayGames.PlayGamesPlatform.Activate ();
				
				LogInGooglePlus ();
		}

			
		public void ToggleLogin ()
		{
				if (!Social.localUser.authenticated) {
						LogInGooglePlus ();
				} else {
						LogOutGooglePlus ();
				}
		}
		public void LogInGooglePlus ()
		{
				Social.localUser.Authenticate ((bool success) => {
						if (success) {
								//	mStatusText = "Welcome " + Social.localUser.userName;
								
						} else {
								//	mStatusText = "Authentication failed.";
								
						}
				});
				
		}
		public void LogOutGooglePlus ()
		{
				((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut ();
			
		}

		public void ShowAchivments ()
		{
				Social.ShowAchievementsUI ();
		}
		public void AddOneStar ()
		{
				PlayGamesPlatform.Instance.IncrementAchievement (
			STAR_15, 1, (bool success) => {
						// handle success or failure
				});
		}
		public void ShowLeaderboard ()
		{
				Social.ShowLeaderboardUI ();
		}
		public void PostScoreToLeaderboard (int level, long time)
		{
				string id = " ";
				switch (level) {
				case 1: 
						id = LEVEL_1;
						break;
				case 2: 
						id = LEVEL_2;
						break;
				case 3: 
						id = LEVEL_3;
						break;
				case 4: 
						id = LEVEL_4;
						break;
				case 5: 
						id = LEVEL_5;
						break;
				case 6: 
						id = LEVEL_6;
						break;
				case 7: 
						id = LEVEL_7;
						break;
				case 8: 
						id = LEVEL_8;
						break;
				case 9: 
						id = LEVEL_9;
						break;
				case 10: 
						id = LEVEL_10;
						break;
				case 11: 
						id = LEVEL_11;
						break;
				case 12: 
						id = LEVEL_12;
						break;
				case 13: 
						id = LEVEL_13;
						break;
				case 14: 
						id = LEVEL_14;
						break;
				case 15: 
						id = LEVEL_15;
						break;
				case 16: 
						id = LEVEL_16;
						break;
				case 17: 
						id = LEVEL_17;
						break;
				case 18: 
						id = LEVEL_18;
						break;
				case 19: 
						id = LEVEL_19;
						break;
				case 20: 
						id = LEVEL_20;
						break;
				}

				if (!id.Equals (" ")) {
						Social.ReportScore (time, id, (bool success) => {
								// handle success or failure
						});
				}
				
		}
}
