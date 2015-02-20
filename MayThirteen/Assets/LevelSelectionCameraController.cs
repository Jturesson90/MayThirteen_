using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class LevelSelectionCameraController : MonoBehaviour
{
		GameMovement gameMovement;
		public float startZoomAfter = 2.5f;
		// Use this for initialization
		
		void Awake ()
		{
				gameMovement = GetComponent<GameMovement> ();
				gameMovement.enabled = false;
		}
		void Start ()
		{
				StartZoomAfter (startZoomAfter);
		}
	
		// Update is called once per frame
		void Update ()
		{
				Physics2D.gravity = -transform.up * 9.81f;
		}
		
		public void DoneZooming ()
		{
				//TODO DoneZooming
				print ("DONE ZOOMING");
				gameMovement.enabled = true;
		}
		private void StartZoomAfter (float waitTime)
		{
				gameMovement.enabled = false;
				GetComponent<ZoomToTargetOrtho> ().StartZoomAfter (waitTime);
		}
}
