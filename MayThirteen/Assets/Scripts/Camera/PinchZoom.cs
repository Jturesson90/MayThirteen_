using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
		private float perspectiveZoomSpeed = 0.1f;  
		private float orthoZoomSpeed = 0.1f;
		[Tooltip("Hello")]
		public float
				maximumZoom = 20f;

		void Start ()
		{
			
		}
		void FixMaximumZoom ()
		{
				if (maximumZoom < 0f) {
						maximumZoom = 20f;
				}
		}
		// Update is called once per frame
		void Update ()
		{
				#if UNITY_ANDROID || UNITY_IPHONE
				HandleMobileInput ();
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
				HandleStandaloneInput ();
#endif
		}
		private void HandleMobileInput ()
		{
				if (Input.touchCount == 2) {	

						Touch touchZero = Input.GetTouch (0);
						Touch touchOne = Input.GetTouch (1);

						Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
						Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
						
						float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
						float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
					
						float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
						
						if (GetComponent<Camera> ().orthographic) {
								
								GetComponent<Camera> ().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
							
								GetComponent<Camera> ().orthographicSize = Mathf.Max (GetComponent<Camera> ().orthographicSize, 3f);
								if (GetComponent<Camera> ().orthographicSize > 18f) {
										GetComponent<Camera> ().orthographicSize = 18f;
								}
							
						} else {
								Vector3 newCameraPosition = GetComponent<Camera> ().transform.position;					
								newCameraPosition.z -= deltaMagnitudeDiff * perspectiveZoomSpeed;
								newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -maximumZoom, -3.5f);								
								GetComponent<Camera> ().transform.position = newCameraPosition;
								
				
						}
				}
		}
		private void HandleStandaloneInput ()
		{
				if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
						ZoomIn ();
				} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
						ZoomOut ();
			
				}
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
						ZoomIn ();
				}
				if (Input.GetKeyDown (KeyCode.DownArrow)) {
						ZoomOut ();
				}
		}
		private void ZoomOut ()
		{
				if (GetComponent<Camera> ().orthographic) {	
						GetComponent<Camera> ().orthographicSize ++;
						GetComponent<Camera> ().orthographicSize = Mathf.Max (GetComponent<Camera> ().orthographicSize, 3f);
						if (GetComponent<Camera> ().orthographicSize > 18f) {
								GetComponent<Camera> ().orthographicSize = 18f;
						}
				} else {
						Vector3 newCameraPosition = GetComponent<Camera> ().transform.position;	
						newCameraPosition.z--;
						newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -maximumZoom, -3.5f);								
						GetComponent<Camera> ().transform.position = newCameraPosition;	
			
				}
		}
		private void ZoomIn ()
		{
				if (GetComponent<Camera> ().orthographic) {
						GetComponent<Camera> ().orthographicSize--;
						GetComponent<Camera> ().orthographicSize = Mathf.Max (GetComponent<Camera> ().orthographicSize, 3f);
						if (GetComponent<Camera> ().orthographicSize > 18f) {
								GetComponent<Camera> ().orthographicSize = 18f;
						}
				} else {
						Vector3 newCameraPosition = GetComponent<Camera> ().transform.position;	
						newCameraPosition.z++;
						newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -maximumZoom, -3.5f);								
						GetComponent<Camera> ().transform.position = newCameraPosition;			
				}
		}
}
