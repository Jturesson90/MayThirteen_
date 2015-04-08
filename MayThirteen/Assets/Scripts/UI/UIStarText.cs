using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStarText : MonoBehaviour
{
		private static int NUM_OF_STARS = 20;
		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		void OnEnable ()
		{
				setText (PlayerPrefsManager.GetStars () + "/" + NUM_OF_STARS);
		}
		public void setText (string text)
		{
				transform.GetComponent<Text> ().text = text;
		}
}
