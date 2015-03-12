using UnityEngine;
using System.Collections;

public class LevelSelectionManager : MonoBehaviour
{
		RockPlayer rockPlayer;
		LittleRockstarAds littleRockstarAds;
		void Awake ()
		{		
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
