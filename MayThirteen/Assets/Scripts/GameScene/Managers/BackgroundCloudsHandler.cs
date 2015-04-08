using UnityEngine;
using System.Collections;

public class BackgroundCloudsHandler : MonoBehaviour
{

		public float scrollSpeed = 2f;

		private Vector3 startPosition;
		private float bgWidth;
		void Awake ()
		{
				bgWidth = GetComponentInChildren<SpriteRenderer> ().bounds.size.x;
		}
		void Start ()
		{
				startPosition = transform.position;
		}
	
		void Update ()
		{
				float newPosition = Mathf.Repeat (Time.time * scrollSpeed, bgWidth);
				transform.position = startPosition + Vector3.right * newPosition;
		}
}
