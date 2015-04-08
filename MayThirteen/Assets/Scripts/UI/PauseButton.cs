using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
		public Sprite pauseSprite;
		public Sprite playSprite;

		public void TogglePause ()
		{
				transform.GetComponent<Image> ().sprite = transform.GetComponent<Image> ().sprite == playSprite ? pauseSprite : playSprite;
				
		}
}
