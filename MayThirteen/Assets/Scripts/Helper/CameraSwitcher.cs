using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{

		private Camera mainCamera;
		private Camera secondaryCamera;

		void Awake ()
		{
				mainCamera = Camera.main;
				secondaryCamera = GameObject.FindGameObjectWithTag ("Camera2").GetComponent<Camera>();
		}
		void Update ()
		{
				if (Application.loadedLevelName == "LevelX")
						return;
				if (Time.timeScale == 0.0f && mainCamera.enabled) {
						secondaryCamera.enabled = true;
						mainCamera.enabled = false;
						
				} else if (Time.timeScale > 0.1f && secondaryCamera.enabled) {
						secondaryCamera.enabled = false;
						mainCamera.enabled = true;

				}
		}
}
