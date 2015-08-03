using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
		public Sprite pauseSprite;
		public Sprite playSprite;
		Image image;
		
		void Awake ()
		{
				image = GetComponent<Image> ();
		}
		public void TogglePause ()
		{
				//	GetComponent<Image> ().sprite = GetComponent<Image> ().sprite == playSprite ? pauseSprite : playSprite;
				
		}
		void Update ()
		{
				if (Time.timeScale == 0) {
						if (image.sprite != playSprite) {
								image.sprite = playSprite;
								print ("CHANGE");

						}
				} else {
						if (image.sprite != pauseSprite) {
								image.sprite = pauseSprite;
								print ("CHANGE");
						}
				}
		}
}
