using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{

		public Sprite offSprite;
		public Sprite onSprite;
		
		void Awake ()
		{
				AudioListener.pause = true;
			
				if (PlayerPrefsManager.IsSoundOn ()) {
						AudioListener.pause = false;
					
				} else {
						AudioListener.pause = true;
						
				}


				transform.GetComponent<Image> ().sprite = !AudioListener.pause ? onSprite : offSprite;
		}
		void Start ()
		{
				
		}
		public void ToggleMute ()
		{
				AudioListener.pause = AudioListener.pause ? false : true;
				if (AudioListener.pause) {
						PlayerPrefsManager.SetSoundOff ();
				} else {
						PlayerPrefsManager.SetSoundOn ();
				}

				transform.GetComponent<Image> ().sprite = !AudioListener.pause ? onSprite : offSprite;
		}
		

}
