using UnityEngine;
using System.Collections;


public class Earthquake2 : MonoBehaviour
{
		enum Fade
		{
				In,
				Out
		}
		public bool shake = false;
		
		void Awake ()
		{	
				
			
		}
		// Use this for initialization
		void Start ()
		{
	
		}

		void HandleAudio ()
		{
				if (Time.timeScale == 0.0 && audio.isPlaying) {
						audio.Pause ();
				} else if (Time.timeScale == 1.0 && !audio.isPlaying) {
						audio.Play ();
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (shake) {
						ShakeWithAmount (5f);
				}	
				HandleAudio ();
		}

		public void StartEarthquake ()
		{

		}
		public void EndEarthquake ()
		{

		}
		private void Vibrate ()
		{
				#if UNITY_ANDROID || UNITY_IPHONE
				Handheld.Vibrate ();
				#endif
		}
		public bool ShakeForSeconds (float seconds)
		{
				return false;
		}
		private void ShakeWithAmount (float shakeAmount)
		{
				Vector3 shakeVector3 = Random.insideUnitSphere * shakeAmount;
				shakeVector3.z = 0f;
				Camera.main.transform.localPosition += shakeVector3;
				Vibrate ();
		}
		private void FadeAudio (float timer, Fade fadeType)
		{
				float start = fadeType == Fade.In ? 0.0f : 1.0f;
				float end = fadeType == Fade.In ? 1.0f : 0.0f;
				float i = 0.0f;
				float step = 1.0f / timer;
		
				while (i <= 1.0f) {
						i += step * Time.deltaTime;
						audio.volume = Mathf.Lerp (start, end, i);
						Yield ();
				}
		}
		private IEnumerator Yield ()
		{
				yield return null;
		}
}
