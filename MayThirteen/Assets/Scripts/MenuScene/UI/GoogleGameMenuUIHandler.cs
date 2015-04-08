using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
public class GoogleGameMenuUIHandler : MonoBehaviour
{

		Image loginUI;
		Button gameUI;
		Color normal = new Color (1f, 1f, 1f, 1f);
		Color faded = new Color (1f, 1f, 1f, 0.5f);
		LittleRockstarGoogleGame googleGame;
		// Use this for initialization
		
		void Awake ()
		{
				googleGame = GameObject.Find ("LittleRockstarGoogleGame").GetComponent<LittleRockstarGoogleGame> ();
				gameUI = GameObject.Find ("GoogleGame").GetComponent<Button> ();
				loginUI = GameObject.Find ("GooglePlusLogin").GetComponent<Image> ();
		}
		void Start ()
		{
				
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (IsLoggedIn ()) {
						gameUI.interactable = true;
						loginUI.color = faded;
	 		
				} else {
						loginUI.color = normal;
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
		bool IsLoggedIn ()
		{
				return Social.localUser.authenticated;
		}
}
