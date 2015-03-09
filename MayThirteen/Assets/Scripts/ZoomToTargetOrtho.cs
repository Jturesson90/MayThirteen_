using UnityEngine;
using System.Collections;

public class ZoomToTargetOrtho : MonoBehaviour
{
		private LevelSelectionCameraController controller;
		public Transform toTarget;
		private Transform fromTarget;
		private Vector3 startPos;

		private float startOrthoSize;
		public float orthoSizeTarget = 10f;

		public float minZoomSpeed = 3f;
		public float extraSpeedIfClicked = 6f;
		
		private float extraSpeed = 1f;
		
		private float speed;
		
		private bool clicked = false;
		private bool doneZooming = true;

		void Awake ()
		{
				startOrthoSize = GetComponent<Camera>().orthographicSize;
				fromTarget = transform;
				startPos = fromTarget.position;
				controller = GetComponent<LevelSelectionCameraController> ();
		}
		void Start ()
		{
				
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Clicked ()) {
						extraSpeed = extraSpeedIfClicked;
						clicked = true;
				}

		}
		private bool Clicked ()
		{
				#if UNITY_STANDALONE || UNITY_EDITOR
				if (Input.anyKeyDown) {
						
						return true;
				}
				#endif
				#if UNITY_ANDROID || UNITY_IPHONE
				if (Input.touchCount > 0) {
						return true;
				}
				#endif
				return false;
		}

		public void StartZoomAfter (float seconds)
		{
				//StartCoroutine (Wait (seconds));
				StartCoroutine (StartZoomAfterCanEnd (seconds));
		}
		IEnumerator StartZoomAfterCanEnd (float seconds)
		{
				float time = 0f;
				clicked = false;
				while ((!clicked && time < seconds)|| time < 1f) {
						time += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				StartCoroutine (StartZoom ());

		}
		IEnumerator Wait (float waitTime)
		{
				yield return new WaitForSeconds (waitTime);
				StartCoroutine (StartZoom ());
				
		}
		
		private IEnumerator StartZoom ()
		{
				
				
				doneZooming = false;
				while (!doneZooming) {
						float distance = Vector3.Distance (fromTarget.position, toTarget.position);
						transform.position = fromTarget.position;
						speed = distance;

						if (speed < minZoomSpeed) {
								speed = minZoomSpeed;
						}

						
						Vector3 dir = toTarget.position - fromTarget.position;
						dir = dir.normalized;
						fromTarget.Translate (extraSpeed * dir * speed * Time.deltaTime, Space.World);
						
						if (GetComponent<Camera>().orthographic) {
								GetComponent<Camera>().orthographicSize = CalculateOrthoSize ();
						}
						
					
						if (distance < 0.5f) {
								doneZooming = true;
						}
						
						yield return new WaitForFixedUpdate ();
				}
				DoneZooming ();
				
		}

		private float CalculateOrthoSize ()
		{
				float sizeToReturn = 0f;
				Vector3 posStart = startPos;
				Vector3 posEnd = toTarget.position;
				Vector3 posNow = fromTarget.position;
				
				float fullDistance = Vector3.Distance (posStart, posEnd);
				float currentDistance = Vector3.Distance (posNow, posEnd);
				float distancePercentage = currentDistance / fullDistance;

				
				float orthoSizeEnd = orthoSizeTarget;
				
				float fullSizeDiff = startOrthoSize - orthoSizeEnd;
		
				sizeToReturn = orthoSizeEnd + (fullSizeDiff * distancePercentage);
				
				return sizeToReturn;
		}
		private void DoneZooming ()
		{
				GetComponent<Camera>().orthographicSize = orthoSizeTarget;
				controller.DoneZooming ();
		}
}
