using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		public Transform secondCameraPosition;
		private bool cameraHelper = false;
		private Transform savedTransform;
		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Application.loadedLevelName == "LevelX")
						return;
				if (Time.timeScale == 0.0f) {
						gameObject.transform.parent.position = secondCameraPosition.position;

				}
				/*	if (Time.timeScale == 0.0f && !cameraHelper) {
						cameraHelper = true;
				} else if (Time.timeScale > 0.1f && cameraHelper) {
						cameraHelper = false;
				}*/

		}
}
