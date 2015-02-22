using UnityEngine;
using System.Collections;

public class ZoomToTargetPerspective : MonoBehaviour
{

		private GameCameraController controller;

		public Transform toTarget;

		private Transform fromTarget;


	
		public float minZoomSpeed = 3f;
		public float extraSpeedIfClicked = 6f;
	
		private float extraSpeed = 1f;
	
		private float speed;
	
		private bool clicked = false;

		private bool doneZooming = true;
	
		void Awake ()
		{
				fromTarget = transform;

				controller = GetComponent<GameCameraController> ();
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
						if (distance < 0.5f) {
								doneZooming = true;
						}
			
						yield return new WaitForFixedUpdate ();
				}
				DoneZooming ();
		
		}
	
		
		private void DoneZooming ()
		{
				
				controller.DoneZooming ();
		}
}
