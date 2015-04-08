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
				//transform.position = player.transform.position;
				//Vector3 newPosition = new Vector3 ((player.transform.position.x) - camera.transform.position.x, (player.transform.position.y + 0.5f) - camera.transform.position.y, player.transform.position.z - camera.transform.position.z);
				//Vector3 newPosition = new Vector3 ((camera.transform.position.x) - player.transform.position.x, (camera.transform.position.y) - (player.transform.position.y) + 1.5f, camera.transform.position.z - player.transform.position.z);
				Vector3 abovePlayerPosition = new Vector3 (player.transform.position.x - 0.25f, player.transform.position.y + 1f, player.transform.position.z);
				Vector3 newPosition = abovePlayerPosition - camera.transform.position;
				newPosition.Normalize ();
				transform.position += newPosition * 25f;


		}
}
