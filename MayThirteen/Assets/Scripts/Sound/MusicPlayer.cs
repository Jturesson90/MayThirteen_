using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
		enum SongType
		{
				LOW,
				MIDDLE,
				HIGH
		}

		public float currentTime;
		private AudioClip currentClip;
		public static MusicPlayer instance;
		public AudioClip[] backgroundMusic;

		string currentScene = "";

		
		// Use this for initialization
		void Awake ()
		{
				if (instance != null && instance != this) {
						Destroy (this.gameObject);
						return;
				} else {
						instance = this;
				


				}
				DontDestroyOnLoad (this.gameObject);
				AudioListener.volume = 0f;
		}
		void Start ()
		{
				AudioListener.pause = !PlayerPrefsManager.IsSoundOn ();
				AudioListener.volume = 1f;
		}
	#if UNITY_EDITOR
		void Update ()
		{

				if (currentScene != Application.loadedLevelName) {
						currentScene = Application.loadedLevelName;
						CheckLevel ();
				}

				currentTime = GetComponent<AudioSource> ().time;
		}
	#else
	void OnLevelWasLoaded ()
	{
		CheckLevel ();
	}
	#endif
		
		void CheckLevel ()
		{
				switch (Application.loadedLevelName) {
			
				case  "LevelX":
						//RandomSongType (SongType.LOW);	
						break;
			
				case "Menu":
						PlaySong (0);
						break;
			
				case "LevelSelectionLobby":
				
						int levelsDone = PlayerPrefsManager.GetLevelsDone ();
						if (levelsDone > 9) {
								
								PlaySong (9);
						} else if (levelsDone > 8) {
								RandomSongType (SongType.HIGH);
						} else if (levelsDone > 4) {
				
								RandomSongType (SongType.MIDDLE);
						} else {
								RandomSongType (SongType.LOW);
						}
						break;
				case "Splash":
						break;
				default :
						var currentLevel = PlayerPrefsManager.GetCurrentLevel ();
						if (currentLevel > 9) {
								//RandomSongType (SongType.HIGH);
								PlaySong (9);
						} else if (currentLevel > 8) {
								RandomSongType (SongType.HIGH);
						} else if (currentLevel > 4) {
								RandomSongType (SongType.MIDDLE);
						} else {
								RandomSongType (SongType.LOW);
						}
						break;
				}
		
		}
		void PlaySong (int id)
		{
				float timeInSamples = GetComponent<AudioSource> ().time;
				AudioSource audioSource = GetComponent<AudioSource> ();
				float timeSince = Time.time;
				audioSource.clip = backgroundMusic [id];
				if (audioSource.clip != currentClip) {
			
						currentClip = audioSource.clip;
						
						audioSource.Play ();
		
						
						timeSince = Time.time - timeSince;
			
						audioSource.time = timeInSamples + timeSince;
				}
		}
		void RandomSongType (SongType type)
		{
				switch (type) {
				case SongType.LOW:
						PlaySong (Random.Range (0, 3));
			
						break;
			
				case SongType.MIDDLE: 
						PlaySong (Random.Range (4, 7));
			
						break;
			
				case SongType.HIGH:
			
						PlaySong (Random.Range (8, 9));
			
						break;
				default:
						break;
				}
		}
}
