using UnityEngine;
using System.Collections;

public class GoogleGameSingleton : MonoBehaviour
{
		public static GoogleGameSingleton googleGame;
		// Use this for initialization

		void Awake ()
		{
		
				if (googleGame == null) {
						DontDestroyOnLoad (this.gameObject);
						googleGame = this;
				} else if (googleGame != this) {
						Destroy (gameObject);
				}

		}
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
