using UnityEngine;
using System.Collections;


public class MathTestConverter : MonoBehaviour
{

		public float myFloat = 0;
		public long myLong = 0;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				myLong = System.Convert.ToInt64 (myFloat * 1000);

				
		}
}
