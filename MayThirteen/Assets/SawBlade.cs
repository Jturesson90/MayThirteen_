using UnityEngine;
using System.Collections;

public class SawBlade : MonoBehaviour
{

		public float spinSpeed = -250f;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				transform.Rotate (0, 0, Time.deltaTime * spinSpeed, Space.World);
		}
		void OnCollisionEnter2D (Collision2D coll)
		{
				if (coll.gameObject.tag == "Ball") {
						coll.gameObject.SendMessage ("Die", transform.position);
				}
		}
}
