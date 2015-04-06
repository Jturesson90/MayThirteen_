using UnityEngine;
using System.Collections;

public class LevelSelectionManager : MonoBehaviour
{
		RockPlayer rockPlayer;
		LittleRockstarAds littleRockstarAds;
		public GameObject backgroundClouds;
		void Awake ()
		{		
				backgroundClouds.SetActive (false);
				int levelsDone = PlayerPrefsManager.GetLevelsDone ();
				if (levelsDone >= 10) {
						print ("LevelsDone TRUE");
						backgroundClouds.SetActive (true);
				} else {
						backgroundClouds.SetActive (false);
						print ("LevelsDone FALSE");
				}
				
				littleRockstarAds = GameObject.FindGameObjectWithTag ("Ads").GetComponent<LittleRockstarAds> ();
				rockPlayer = GameObject.Find ("RockPlayer").GetComponent<RockPlayer> ();
		}
		void Start ()
		{
				
				Invoke ("ShowAds", 2f);
				Time.timeScale = 1f;
		}
		public void LoadLevel (string level)
		{
				rockPlayer.SavePosition ();
				LevelSwitcher.levelSwitcher.SwitchLevel (level);
				
		}
		public void LoadMenu ()
		{
				LevelSwitcher.levelSwitcher.SwitchLevel ("Menu");
		}
		
		private void ShowAds ()
		{
				littleRockstarAds.ShowAds ();
		}

}
