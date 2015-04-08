using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour
{

		public Transform followTarget;
		public float extraSpeed = 5f;
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		void FixedUpdate ()
		{
				transform.Rotate (Vector3.forward * Time.deltaTime * extraSpeed);
				transform.position = new Vector3 (followTarget.position.x, followTarget.position.y, transform.position.z);
				Physics2D.gravity = -transform.up * 9.81f;
		}
}
