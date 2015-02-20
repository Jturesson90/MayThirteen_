using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEnterLevelText : MonoBehaviour
{
		void Awake ()
		{
				setText ("");
		}
		public void setText (string text)
		{
				transform.GetComponent<Text> ().text = text;
		}

}
