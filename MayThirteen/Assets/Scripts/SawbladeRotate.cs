using UnityEngine;
using System.Collections;

public class SawbladeRotate : MonoBehaviour
{
		public float spinSpeed = -600f;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				transform.Rotate (0, 0, Time.deltaTime * spinSpeed, Space.World);
		}
}
