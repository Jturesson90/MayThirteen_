using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class LittleRockstarGoogleGame : MonoBehaviour
{

		


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
}
