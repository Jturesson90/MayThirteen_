using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour
{
		bool screenshot = false;
		int nr = 3;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.B))
						screenshot = true;
				if (screenshot) {
						//yield WaitForEndOfFrame();
						// Create a texture the size of the screen, RGB24 format
						var width = Screen.width;
						var height = Screen.height;
						var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
						// Read screen contents into the texture
						tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
						tex.Apply ();
						// Encode texture into PNG
						var bytes = tex.EncodeToPNG ();
						Destroy (tex);
						
						File.WriteAllBytes (Application.dataPath + "/../SavedScreen" + nr + ".png", bytes);
						nr++;
						screenshot = false;
				}
		}

}
