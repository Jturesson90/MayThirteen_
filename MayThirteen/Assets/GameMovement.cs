using UnityEngine;
using System.Collections;

public class GameMovement : MonoBehaviour
{


		private PinchZoom pinchZoom;
		private FollowTarget followTarget;
		public float turnSpeed = 55f;
		public float touchOffset = 0.25f;
	

		private void Awake ()
		{
				
				followTarget = gameObject.GetComponent<FollowTarget> ();
				pinchZoom = gameObject.GetComponent<PinchZoom> ();
				
				
		}

	
		// Update is called once per frame
		void Update ()
		{
#if UNITY_STANDALONE || UNITY_EDITOR	
				StandAloneMovement ();
#endif
				#if UNITY_ANDROID || UNITY_IPHONE
				MobileMovement ();
#endif




		}
		void MobileMovement ()
		{
				if (Input.touchCount == 2 || Input.touchCount < 1)
						return;
				
				if (Input.GetTouch (0).position.x < Screen.width * touchOffset) {
						transform.Rotate (0, 0, -Time.deltaTime * turnSpeed);
					
				}
				if (Input.GetTouch (0).position.x > Screen.width * (1 - touchOffset)) {
						transform.Rotate (0, 0, Time.deltaTime * turnSpeed);
						
				}
		}

		void StandAloneMovement ()
		{
				if (Input.GetKey (KeyCode.LeftArrow)) {
						transform.Rotate (0, 0, -Time.deltaTime * turnSpeed);
						
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
						transform.Rotate (0, 0, Time.deltaTime * turnSpeed);
						
				}
		}
		void StartGame ()
		{

		}
		void OnDisable ()
		{
				EnablePinchZoom (false);
				if (Application.loadedLevelName == "LevelSelectionLobby") {
						GetComponent<LevelSelectionCameraController> ().HideUIArrows ();
				} else if (Application.loadedLevel > 3) {
						
						GetComponent<GameCameraController> ().HideUIArrows ();					
				}
		}
		void OnEnable ()
		{
				EnablePinchZoom (true);
				if (Application.loadedLevelName == "LevelSelectionLobby") {
						GetComponent<LevelSelectionCameraController> ().ShowUIArrows ();
				} else {
						GetComponent<GameCameraController> ().ShowUIArrows ();
				}
		}
		
		private void EnablePinchZoom (bool arg)
		{
				pinchZoom.enabled = arg;
				followTarget.enabled = arg;
		}
		
}
