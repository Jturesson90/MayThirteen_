using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
		private float perspectiveZoomSpeed = 0.1f;  
		private float orthoZoomSpeed = 0.1f;     

		void Start ()
		{
	
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
			
						
						if (camera.isOrthoGraphic) {
								
								camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
							
								camera.orthographicSize = Mathf.Max (camera.orthographicSize, 3f);
								if (camera.orthographicSize > 18f) {
										camera.orthographicSize = 18f;
								}
							
						} else {
								Vector3 newCameraPosition = camera.transform.position;					
								newCameraPosition.z -= deltaMagnitudeDiff * perspectiveZoomSpeed;
								newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -20f, -3.5f);								
								camera.transform.position = newCameraPosition;
								
				
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
				if (camera.isOrthoGraphic) {	
						camera.orthographicSize ++;
						camera.orthographicSize = Mathf.Max (camera.orthographicSize, 3f);
						if (camera.orthographicSize > 18f) {
								camera.orthographicSize = 18f;
						}
				} else {
						Vector3 newCameraPosition = camera.transform.position;	
						newCameraPosition.z--;
						newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -20f, -3.5f);								
						camera.transform.position = newCameraPosition;	
			
				}
		}
		private void ZoomIn ()
		{
				if (camera.isOrthoGraphic) {
						camera.orthographicSize--;
						camera.orthographicSize = Mathf.Max (camera.orthographicSize, 3f);
						if (camera.orthographicSize > 18f) {
								camera.orthographicSize = 18f;
						}
				} else {
						Vector3 newCameraPosition = camera.transform.position;	
						newCameraPosition.z++;
						newCameraPosition.z = Mathf.Clamp (newCameraPosition.z, -20f, -3.5f);								
						camera.transform.position = newCameraPosition;			
				}
		}
}
