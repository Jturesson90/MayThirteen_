using UnityEngine;
using System.Collections;

public class ExplodeScript : MonoBehaviour
{
		public Transform explosionTransform;
		public float _explosionForce = 5f;
		public float _explosionRadius = 5f;
		// Use this for initialization
		void Start ()
		{
				foreach (Transform child in transform) {
						if (!child.name.Equals ("Middle")) {
								AddExplosionForce (child.GetComponent<Rigidbody2D>(), _explosionForce, explosionTransform.position, _explosionRadius);
						}
				}
		}
	
		public static void AddExplosionForce (Rigidbody2D body, float _explosionForce,
	                                     Vector3 explosionPosition, float _explosionRadius)
		{
				var dir = (body.transform.position - explosionPosition);
				float wearoff = 1 - (dir.magnitude / _explosionRadius);
				body.AddForce (dir.normalized * _explosionForce * wearoff);
		}
}
