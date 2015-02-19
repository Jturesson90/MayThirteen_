using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GUITexture))]
public class FadeInScript : MonoBehaviour
{
		public float fadeSpeed = 5f;
		public bool fadeOut = false;
		private string levelString = "Menu";
		private float savedHeight;
		private bool newLevel = false;
		
	
		void Awake ()
		{
				
				guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
				savedHeight = Screen.height;
				Time.timeScale = 1;
		}
		void Update ()
		{
				if (Screen.height != savedHeight)
						guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
		
				if (fadeOut) {
						FadeToBlack ();
				} else {
						FadeToClear ();
				}
		}
		public void FadeToLevel (string str)
		{
				fadeOut = true;
				newLevel = true;
				Color color = guiTexture.color;
				color.a = 0f;
				guiTexture.color = color;
				
				levelString = str;
		}
		void FadeToClear ()
		{
				Color guiTextureColor = guiTexture.color;
				if (guiTextureColor.a < 0.05f) {
						guiTextureColor.a = 0f;
						guiTexture.color = guiTextureColor;
						return;
				}
				guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
		void FadeToBlack ()
		{
		
				guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
		
				if (guiTexture.color.a > 0.95f && newLevel) {
						fadeOut = false;
						Application.LoadLevel (levelString);
						newLevel = false;
				}
		}
}

