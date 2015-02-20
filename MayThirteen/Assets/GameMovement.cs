using UnityEngine;
using System.Collections;

public class GameMovement : MonoBehaviour
{

		private UIHelper uiHelper;
		private PinchZoom pinchZoom;
		private FollowTarget followTarget;
		public float turnSpeed = 55f;
		public float touchOffset = 0.25f;
		
		private void Awake ()
		{
				followTarget = gameObject.GetComponent<FollowTarget> ();
				pinchZoom = gameObject.GetComponent<PinchZoom> ();
				uiHelper = GameObject.Find ("UIHelper").GetComponent<UIHelper> ();
				
		}
		// Use this for initialization
		void Start ()
		{
				
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
		void OnDisable ()
		{
				uiHelper.HideArrows ();
				EnablePinchZoom (false);
		}
		void OnEnable ()
		{
				uiHelper.ShowArrows ();
				EnablePinchZoom (true);
		}
		private void EnablePinchZoom (bool arg)
		{
				pinchZoom.enabled = arg;
				followTarget.enabled = arg;
		}
		
}
