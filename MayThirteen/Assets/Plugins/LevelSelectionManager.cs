using UnityEngine;
using System.Collections;

public class LevelSelectionManager : MonoBehaviour
{
		RockPlayer rockPlayer;

		void Awake ()
		{		
			
				rockPlayer = GameObject.Find ("RockPlayer").GetComponent<RockPlayer> ();
		}
		void Start ()
		{
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

}
