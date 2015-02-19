using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour
{

		public static GameUIController gameUIController;
		// Use this for initialization
		void Awake ()
		{
				
				if (gameUIController != null && gameUIController != this) {
						Destroy (this.gameObject);
						return;
				} else {
						gameUIController = this;
				}
		
		
				DontDestroyOnLoad (this.gameObject);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Application.loadedLevelName == "Menu") {
						Destroy (this.gameObject);
				}
		}
}
