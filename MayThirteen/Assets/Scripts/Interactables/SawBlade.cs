using UnityEngine;
using System.Collections;

public class SawBlade : MonoBehaviour
{

		public float spinSpeed = -250f;
		// Use this for initialization
		private GameManageHandler manager;
		//private GameObject sawbladeExplosion;
		void Awake ()
		{	
				manager = GameObject.Find ("GameManageHandler").GetComponent<GameManageHandler> ();
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
						manager.Die ();
#if UNITY_ANDROID && !UNITY_EDITOR
						Vibration.Vibrate (100);
#endif		
				}
				/*			if (sawbladeExplosion) {
						GameObject explosion = Instantiate (sawbladeExplosion, transform.position, Quaternion.identity) as GameObject;
						explosion.transform.parent = transform;
						StartCoroutine (DestroySawBladeExplosion (explosion));
				}
	*/
		}
		IEnumerator DestroySawBladeExplosion (GameObject explosion)
		{
				yield return new WaitForFixedUpdate ();
				//	Destroy (explosion);
		}

}
