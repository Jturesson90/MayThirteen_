using UnityEngine;
using System.Collections;

public class StoneWall : MonoBehaviour
{
		public int wallIndex = 200;
		public float earthquakeLength = 5f;
		private EarthquakeEffect earthquake	;


		void Awake ()
		{
				earthquake = GameObject.Find ("EarthquakeEffect").GetComponent<EarthquakeEffect> ();
		}

		public void StartStoneWallAnimation ()
		{
				
				//earthquake.StartEarthquake ();
				earthquake.StartEarthquakeWithLength (animation.clip.length);
				animation.Play ();
		}
		
		public void DisableStoneWall ()
		{
				gameObject.SetActive (false);
		}



}
