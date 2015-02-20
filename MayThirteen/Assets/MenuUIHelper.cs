using UnityEngine;
using System.Collections;

public class MenuUIHelper : MonoBehaviour
{
	
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Application.Quit ();	

				}
		}
}
