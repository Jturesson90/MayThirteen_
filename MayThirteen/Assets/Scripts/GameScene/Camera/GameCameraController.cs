using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class GameCameraController : MonoBehaviour
{
		GameUIHelper uiHelper;
		GameMovement gameMovement;
		public float startZoomAfter = 2.5f;
		public bool isGoalNormalRotation = true;

		void Awake ()
		{
				uiHelper = GameObject.Find ("GameUIHelper").GetComponent<GameUIHelper> ();
				gameMovement = GetComponent<GameMovement> ();
				gameMovement.enabled = false;
				
		}
		public void Lost ()
		{
				gameMovement.enabled = false;
		}
		public void Win ()
		{
				gameMovement.enabled = false;
				if (isGoalNormalRotation) {
						StartCoroutine (RotateToZero ());
				} else {
						StartCoroutine (RotateToUpsideDown ());
				}
		}
		private IEnumerator RotateToUpsideDown ()
		{
				Quaternion cameraWonTargetRotation = Quaternion.Euler (0f, 0f, 180f);
				while (true) {
						transform.rotation = Quaternion.Lerp (transform.rotation, cameraWonTargetRotation, Time.deltaTime * 2.0f);
						yield return new WaitForFixedUpdate ();
				} 
				//transform.rotation =  Quaternion.Lerp(transform.rotation,cameraWonTarget, Time.deltaTime * 2.0);
		}
		private IEnumerator RotateToZero ()
		{
				Quaternion cameraWonTargetRotation = Quaternion.Euler (0f, 0f, 0f);
				while (true) {
						transform.rotation = Quaternion.Lerp (transform.rotation, cameraWonTargetRotation, Time.deltaTime * 2.0f);
						yield return new WaitForFixedUpdate ();
				} 
				//transform.rotation =  Quaternion.Lerp(transform.rotation,cameraWonTarget, Time.deltaTime * 2.0);
		}
		void Start ()
		{
				StartZoomAfter (startZoomAfter);
		}
	
		void Update ()
		{
				Physics2D.gravity = -transform.up * 9.81f;
		}



		public void DoneZooming ()
		{
				gameMovement.enabled = true;
		}
		private void StartZoomAfter (float waitTime)
		{
				gameMovement.enabled = false;
				GetComponent<ZoomToTargetPerspective> ().StartZoomAfter (waitTime);
		}

		public void HideUIArrows ()
		{
				uiHelper.HideArrows ();
		}
		public void ShowUIArrows ()
		{
				uiHelper.ShowArrows ();
		}
}
