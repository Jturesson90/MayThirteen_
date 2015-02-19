using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour
{
		private float speed = 1.5f;
		private bool sceneStarting = true;
		private bool done = false;
		private bool startGameHelper = true;
		public bool testing;
		// Use this for initialization

		void Awake ()
		{
				Camera cam = Camera.main;
				transform.localScale = new Vector3 (cam.orthographicSize / 2 * (Screen.width / Screen.height), cam.orthographicSize / 2, 0f);

				renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);
		}

		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (!done) {
						
						if (sceneStarting) {
						
								StartScene ();		
						} else {
								EndScene ();		
						}		
						//FadeOut ();	
						//renderer.material.color = new Color (Random.value,Random.value,Random.value,1);
				} else {
						if (startGameHelper) {
								StartGame ();
								startGameHelper = false;
						}
				}
				
		}

		void FadeIn ()
		{
				Debug.Log ("FADE_IN");
				renderer.material.color = Color.Lerp (renderer.material.color, new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1), speed * Time.deltaTime);
		}

		void FadeOut ()
		{
				print ("Fade OUT");
				renderer.material.color = Color.Lerp (renderer.material.color, new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0), speed * Time.deltaTime);
		}

		void OnGUI ()
		{
				//GUI.matrix = Matrix4x4.TRS (new Vector3 (0, 0, 0), Quaternion.identity, new Vector3 (Screen.width / 1920, Screen.height / 1080, 1));
				
		}

		void StartScene ()
		{
				FadeIn ();
				if (renderer.material.color.a >= 0.95f) {
						sceneStarting = false;
				}
		}

		void EndScene ()
		{
				FadeOut ();
				if (renderer.material.color.a <= 0.05f) {
						sceneStarting = false;
						done = true;
						
				}
		}

		void StartGame ()
		{
				Application.LoadLevel ("Menu");		
			
		}
}
