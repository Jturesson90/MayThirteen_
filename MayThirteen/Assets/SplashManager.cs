using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour
{
		float startGameIn = 2f;


		void Start ()
		{
				Invoke ("StartGame", startGameIn);
		}

		public void StartGame ()
		{
				LevelSwitcher.levelSwitcher.SwitchLevel ("Menu");
				//Application.LoadLevel ("Menu");
		}
}
