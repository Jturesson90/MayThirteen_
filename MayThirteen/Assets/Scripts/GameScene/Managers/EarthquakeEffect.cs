using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class EarthquakeEffect : MonoBehaviour
{
		[Range(0.0f, 1.0f)]
		public float
				decayEffect;
		public float amountOfShaking = 0.4f;

		private float shakeAmount;
		private float startAudioVolume;
		private bool audioIsPlaying = false;

		void Awake ()
		{
				startAudioVolume = GetComponent<AudioSource> ().volume;
		}
		void FixedUpdate ()
		{
				
		}

		public void StartEarthquakeAfterSeconds (float seconds)
		{
				if (Camera.main == null)
						return;
				StartCoroutine (WaitEarthquake (seconds, false, 0f));
		}
		public void StartEarthquakeAfterSecondsWithLength (float seconds, float time)
		{
				if (Camera.main == null)
						return;		
				StartCoroutine (WaitEarthquake (seconds, true, time));
		}
		
		private IEnumerator WaitEarthquake (float seconds, bool useTime, float time)
		{	
				yield return new WaitForSeconds (seconds);
				StartCoroutine (EarthQuake (time, useTime));
		}


		public void StartEarthquake ()
		{
				if (Camera.main == null)
						return;		
				StartCoroutine (EarthQuake (0f, false));
		}
		public void StartEarthquakeWithLength (float seconds)
		{
				if (Camera.main == null)
						return;		
				StartCoroutine (EarthQuake (seconds, true));
		}
		void Update ()
		{
				CheckAudio ();
				CheckVibration ();
		}
		
		IEnumerator EarthQuake (float time, bool useTime)
		{
			
				
				GetComponent<AudioSource> ().volume = startAudioVolume;
				GetComponent<AudioSource> ().Play ();
				audioIsPlaying = true;
			
				float length;
				float timer = 0f;	

				shakeAmount = amountOfShaking;

				
				if (time > GetComponent<AudioSource> ().clip.length) {
						time = GetComponent<AudioSource> ().clip.length;
				}
				length = useTime ? time : GetComponent<AudioSource> ().clip.length;
				float decayLenghtInSeconds = length * decayEffect;

				float speed = 0f;
				while (timer < length) {
						
						
						ShakeWithAmount (shakeAmount);
						if (timer > (length - decayLenghtInSeconds)) {
								

								float timeLeft = 1.0f / decayLenghtInSeconds;

								speed += timeLeft * Time.deltaTime;
								
								shakeAmount = Mathf.Lerp (amountOfShaking, 0, speed);
								GetComponent<AudioSource> ().volume = Mathf.Lerp (startAudioVolume, 0, speed);
						
						} 
						
						timer += Time.deltaTime;
						
						yield return new WaitForFixedUpdate ();
						
				}

			
				if (GetComponent<AudioSource> ().isPlaying) {
						GetComponent<AudioSource> ().Stop ();
						audioIsPlaying = false;
				}
		}

		private void CheckAudio ()
		{
				if (Time.timeScale == 0.0 && GetComponent<AudioSource> ().isPlaying) {
						
						GetComponent<AudioSource> ().Pause ();
				} else if (Time.timeScale == 1.0 && !GetComponent<AudioSource> ().isPlaying && audioIsPlaying) {
						
						GetComponent<AudioSource> ().Play ();
				}
				
				
		}
		void CheckVibration ()
		{
		}
		private void ShakeWithAmount (float amount)
		{
				Vector2 randomInsideUnitCircle = Random.insideUnitCircle * amount;
				Camera.main.transform.localPosition += new Vector3 (randomInsideUnitCircle.x, randomInsideUnitCircle.y, Camera.main.transform.localPosition.z);
				
		}

		private void Vibrate ()
		{
#if UNITY_EDITOR 

				#elif UNITY_ANDROID || UNITY_IPHONE
				Handheld.Vibrate ();
				#endif
		}
}
