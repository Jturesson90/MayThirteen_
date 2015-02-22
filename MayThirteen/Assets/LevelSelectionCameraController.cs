using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class LevelSelectionCameraController : MonoBehaviour
{
		
		UIHelper uiHelper;
		GameMovement gameMovement;
		public float startZoomAfter = 2.5f;
		
		
		void Awake ()
		{
				uiHelper = GameObject.Find ("UIHelper").GetComponent<UIHelper> ();
				gameMovement = GetComponent<GameMovement> ();
				gameMovement.enabled = false;
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
				GetComponent<ZoomToTargetOrtho> ().StartZoomAfter (waitTime);
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
