using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
public class GoogleGameMenuUIHandler : MonoBehaviour
{

		Image loginUI;
		Button gameUI, leaderboardUI;
		Color normal = new Color (1f, 1f, 1f, 1f);
		Color faded = new Color (1f, 1f, 1f, 0.5f);
		LittleRockstarGoogleGame googleGame;
		// Use this for initialization
		
		void Awake ()
		{
			
				googleGame = GameObject.Find ("LittleRockstarGoogleGame").GetComponent<LittleRockstarGoogleGame> ();
				gameUI = GameObject.Find ("PlayAchievements").GetComponent<Button> ();
				loginUI = GameObject.Find ("PlayLogin").GetComponent<Image> ();
				leaderboardUI = GameObject.Find ("PlayLeaderboard").GetComponent<Button> ();
		}
		void Start ()
		{
				
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (IsLoggedIn ()) {
						gameUI.interactable = true;
						leaderboardUI.interactable = true;
						loginUI.color = faded;
	 		
				} else {
						loginUI.color = normal;
						leaderboardUI.interactable = false;
						gameUI.interactable = false;
						
				}
		}
		public void ToggleLogButton ()
		{
				googleGame.ToggleLogin ();
		}
		public void ShowAchievments ()
		{
				googleGame.ShowAchivments ();
		}
		public void ShowLeaderboard ()
		{
				googleGame.ShowLeaderboard ();
		}
		bool IsLoggedIn ()
		{
				return Social.localUser.authenticated;
		}
}
