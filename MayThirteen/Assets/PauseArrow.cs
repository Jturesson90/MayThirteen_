using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class PauseArrow : MonoBehaviour
{
		void Start ()
		{
				gameObject.SetActive (false);
		}
		void OnEnable ()
		{
				
				GameObject player = GameObject.Find ("RockPlayer");
				GameObject camera = GameObject.Find ("Camera2");
				
				
				transform.position = camera.transform.position;

				Vector3 newPosition = new Vector3 ((player.transform.position.x) - camera.transform.position.x, (player.transform.position.y + 0.5f) - camera.transform.position.y, player.transform.position.z - camera.transform.position.z);
			
				newPosition.Normalize ();
				transform.position += newPosition * 18f;	

		}
}
