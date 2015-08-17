using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour
{
		GameManageHandler manager;
		void Awake ()
		{
				manager = GameObject.Find ("GameManageHandler").GetComponent<GameManageHandler> ();
		}
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Ball") {
						if (manager != null) {
								manager.OutOfBounds ();
						}
				}
		}
}
