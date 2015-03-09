using UnityEngine;
using System.Collections;

public class GameGoal : MonoBehaviour
{

		// Use this for initialization
		GameManageHandler manager;
		
		void Awake ()
		{
				manager = GameObject.Find ("GameManageHandler").GetComponent<GameManageHandler> ();
		}
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		void OnTriggerEnter2D (Collider2D coll)
		{
				if (coll.gameObject.tag == "Ball") {
						GetComponent<Animation>().Play ("PlipAway", PlayMode.StopAll);
						manager.Win ();
				}
		}
}
