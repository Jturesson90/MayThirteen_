﻿using UnityEngine;
using System.Collections;

public class GameManageHandler : MonoBehaviour
{
		GameUIHelper gameUIHelper;
		LevelHandlerC levelHandler;
		GameTimeView gameTime;
		GameObject player;
		
		bool isOver = false;
		
		public float startOffset = 0.3f;

		// Use this for initialization
		void Awake ()
		{
				player = GameObject.Find ("RockPlayer");
				gameTime = GameObject.Find ("GameTimeView").GetComponent<GameTimeView> ();
				levelHandler = GameObject.Find ("LevelHandler").GetComponent<LevelHandlerC> ();
				gameUIHelper = GameObject.Find ("GameUIHelper").GetComponent<GameUIHelper> ();
		}
		void Start ()
		{
				Time.timeScale = 1f;
				StartCoroutine (WaitingForStart ());


				
		}
		IEnumerator WaitingForStart ()
		{
				bool started = false;
				Vector3 startPos = player.transform.position;
				
				while (!started) {
						
						if (HasStarted (startPos)) {
								started = true;
						}
						yield return new WaitForFixedUpdate ();
				}
				StartGame ();
		}
		private bool HasStarted (Vector3 startPos)
		{
				float distance = Vector3.Distance (startPos, player.transform.position);
				if (distance > startOffset) {
						return true;
				}
				return false;
		}
		public void StartGame ()
		{
				
				gameTime.StartTimer ();
		}
		public void Win ()
		{
				if (!isOver) {
						isOver = true;
						gameUIHelper.Win ();
						Camera.main.GetComponent<GameCameraController> ().Win ();

						int currentLevel = PlayerPrefsManager.GetCurrentLevel ();
						bool beatStarTime = gameTime.EndTimerAndDidBeatStarTime ();
						if (beatStarTime) {
								levelHandler.UpdateArray (currentLevel, LevelHandlerC.LevelState.DONE_STAR);
								
						} else {
								levelHandler.UpdateArray (currentLevel, LevelHandlerC.LevelState.DONE);
							
						}
				}
		}
		public void RestartLevel ()
		{
				LevelSwitcher.levelSwitcher.SwitchLevel (Application.loadedLevelName);
		}
		public void ToLevelSelection ()
		{
				LevelSwitcher.levelSwitcher.SwitchLevel ("LevelSelectionLobby");
		}
		
		public void Die ()
		{
				if (!isOver) {
						isOver = true;
						gameUIHelper.Lost ();
						gameTime.Stop ();
						Camera.main.GetComponent<GameCameraController> ().Lost ();
				}
		}

		public void OutOfBounds ()
		{
				if (!isOver) {
						isOver = true;
						gameUIHelper.Lost ();
						gameTime.Stop ();
						Camera.main.GetComponent<GameCameraController> ().Lost ();
				}
		}

}