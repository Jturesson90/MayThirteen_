using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{

		public Sprite offSprite;
		public Sprite onSprite;

		public void ToggleMute ()
		{
				AudioListener.pause = AudioListener.pause ? false : true;
				int soundOn = AudioListener.pause ? 0 : 1;
				PlayerPrefs.SetInt ("SoundOn", soundOn);
				transform.GetComponent<Image> ().sprite = !AudioListener.pause ? onSprite : offSprite;
		}
		

}
