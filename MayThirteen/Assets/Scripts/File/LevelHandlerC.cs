using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelHandlerC : MonoBehaviour
{
		LittleRockstarGoogleGame googleGame;
		
		public static readonly int NUM_OF_LEVELS = 20;
		public enum LevelState
		{
				OPEN,
				NOT_OPEN,
				DONE,
				DONE_STAR}
		;
		public static LevelHandlerC handler;
		private LevelState[] levelArray;


		void Awake ()
		{
				#if UNITY_IPHONE
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
				#endif
				if (Application.loadedLevelName == "Menu") {
						Destroy (gameObject);	
						return;
				}
				if (handler == null) {
						DontDestroyOnLoad (this.gameObject);
						handler = this;
				} else if (handler != this) {
						Destroy (gameObject);
				}
				googleGame = GameObject.Find ("LittleRockstarGoogleGame").GetComponent<LittleRockstarGoogleGame> ();
				Load ();
				
		}

		void LevelsDone ()
		{
				int counter = 0;
				int starCounter = 0;
				for (int i = 0; i < levelArray.Length; i++) {
						if (levelArray [i] == LevelState.DONE || levelArray [i] == LevelState.DONE_STAR) {
								counter++;
						}
						if (levelArray [i] == LevelState.DONE_STAR) {
								starCounter++;
						}
				}
				PlayerPrefsManager.SetStars (starCounter);
				PlayerPrefsManager.SetLevelsDone (counter);
				googleGame.UpdateAchievemnts (starCounter, counter);
		}
		void CheckAgain ()
		{
				if (GameObject.Find ("Levels") != null) {
						foreach (GameObject level in GameObject.FindGameObjectsWithTag("ALevel")) {
								LevelSelectionLevel levelScript = level.GetComponent<LevelSelectionLevel> ();
								levelScript.CheckOpen ();
						}
				}
		}
		public LevelState GetLevelState (int index)
		{
		
				LevelState state = levelArray [index - 1];
		
				return state;
		}
		void OpenNext ()
		{
				Checking (0);
				Checking (5);
				Checking (10);
				OpenFirstFiveIfNotOpen ();
		}
		void OpenFirstFiveIfNotOpen ()
		{
				for (int i = 0; i < 5; i++) {
						if (levelArray [i] == LevelState.NOT_OPEN) {
								levelArray [i] = LevelState.OPEN;
						}
				}
		}
		void Checking (int number)
		{
				bool shouldOpen = true;
				for (int i = number; i < number+5; i++) {
						if (levelArray [i] == LevelState.DONE || levelArray [i] == LevelState.DONE_STAR) {
						} else {
								shouldOpen = false;
						}
				}
				if (shouldOpen) {
						for (int i = number+5; i < number+10; i++) {
								if (levelArray [i] == LevelState.NOT_OPEN)
										levelArray [i] = LevelState.OPEN;
						}
				}
		}
	
		public void UpdateArray (int level, LevelState newState)
		{
				if (levelArray [level - 1] == newState)
						return;
				if (levelArray [level - 1] == LevelState.DONE_STAR)
						return;
				levelArray [level - 1] = newState;
				Save ();
				Load ();
		}
		private Array GetArray ()
		{
				return levelArray;
		}
		void Save ()
		{
				PlayerData oldData = EasySerializer.DeserializeObjectFromFile (Application.persistentDataPath + "/data.dat") as PlayerData;
				if (oldData != null) {
						oldData.levelArray = levelArray;
						EasySerializer.SerializeObjectToFile (oldData, Application.persistentDataPath + "/data.dat");
						print ("GOT DATA, saving old");
				} else {
						PlayerData data = new PlayerData ();
						data.levelArray = levelArray;
						EasySerializer.SerializeObjectToFile (data, Application.persistentDataPath + "/data.dat");
				}
				
				
		}
		void Load ()
		{
				PlayerData data = EasySerializer.DeserializeObjectFromFile (Application.persistentDataPath + "/data.dat") as PlayerData;
				if (data != null) {
						
						levelArray = data.levelArray;
						LevelsDone ();	
						OpenNext ();

				} else {
						levelArray = new LevelState[20];
			
						for (int i = 0; i < levelArray.Length; i++) {
								levelArray [i] = LevelState.NOT_OPEN;
						}
						Save ();
						LevelsDone ();
						OpenNext ();
				}	
		
		}
}





[System.Serializable]
class PlayerData
{
		public LevelHandlerC.LevelState[] levelArray;
}
/*











void Save(){
	BinaryFormatter bf = new BinaryFormatter();
	FileStream file= File.Create(Application.persistentDataPath+"/data.dat");
	
	PlayerData data= new PlayerData();
	data.levelArray = levelArray;
	bf.Serialize(file,data);
	file.Close();
}
void Load(){
	
	if(File.Exists(Application.persistentDataPath+"/data.dat"))
	{
			BinaryFormatter bf= new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath+"/data.dat",FileMode.Open);
		PlayerData data = bf.Deserialize(file);
		file.Close();
		levelArray = data.levelArray;
		LevelsDone();
		OpenNext();	
	}else
	{
		levelArray = new LevelState[20];
		
		for(int i = 0; i < levelArray.ength; i++)
		{
			levelArray[i] = LevelState.NOT_OPEN;
		}
		NewSave();
		//Save();
		LevelsDone();
		OpenNext();
	}	
}
 */