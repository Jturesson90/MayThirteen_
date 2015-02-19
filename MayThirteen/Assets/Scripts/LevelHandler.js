#pragma strict
import System;
import System.IO;
import System.Runtime.Serialization.Formatters.Binary;
public static final var LEVELS = 20;
public enum LevelState{OPEN,NOT_OPEN,DONE,DONE_STAR};
public static var handler : LevelHandler;
private var levelArray : LevelState[];
private var beenHere = false;
function Awake(){
	#if UNITY_IPHONE
	Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
	#endif
	if (Application.loadedLevelName == "Menu") {
		Destroy (gameObject);	
		return;
	}
 	if (handler == null){
       DontDestroyOnLoad(this.gameObject);
       handler = this;
    } else if(handler != this){
        Destroy(gameObject);
    }
	NewLoad();
	//Load();
	var test = false;
	//File.Delete(Application.persistentDataPath+"/data.dat");
	if(test){
		File.Delete(Application.persistentDataPath+"/data.dat");
		for(var i = 0; i < 10; i++){
			levelArray[i] = LevelState.DONE_STAR;		
		}	
		NewSave();
		//Save();
	}
}
function Start () {
	
}

function LevelsDone(){
	var counter : int = 0;
	var starCounter : int =0;
	for(var i = 0; i < levelArray.length ;i++){
		if(levelArray[i] == LevelState.DONE || levelArray[i] == LevelState.DONE_STAR){
			counter++;
		}
		if(levelArray[i] == LevelState.DONE_STAR){
			starCounter++;
		}
	}
	PlayerPrefs.SetInt("Stars",starCounter);
	PlayerPrefs.SetInt("LevelsDone",counter);
}
function CheckAgain(){
	if(GameObject.Find("Levels") != null){
		for(var level : GameObject in GameObject.FindGameObjectsWithTag("ALevel"))
		{
			var levelScript : LevelSelectionScript = level.GetComponent("LevelSelectionScript");
			levelScript.CheckOpen();
		}
	}
}
function OpenNext(){
	Checking(0);
	Checking(5);
	Checking(10);
	for(var i = 0; i < 5;i++){
		if(levelArray[i] == LevelState.NOT_OPEN){
			levelArray[i] = LevelState.OPEN;
			}
	}
}
function Checking ( number: int){
	var shouldOpen = true;
	for(var i = number; i < number+5;i++){
		if(levelArray[i] == LevelState.DONE || levelArray[i] == LevelState.DONE_STAR){
		}else{
			shouldOpen=false;
		}
	}
	if(shouldOpen){
		for(i = number+5; i < number+10;i++){
			if(levelArray[i] == LevelState.NOT_OPEN)
				levelArray[i] = LevelState.OPEN;
		}
		//CheckAgain();
	}
}


function UpdateArray(level : int, newState : LevelState){
	if(levelArray[level-1] == newState)return;
	if(levelArray[level-1] == LevelState.DONE_STAR)return;
	levelArray[level-1] = newState;
	NewSave();
	NewLoad();
	//Save();
	//Load();
}
function GetArray(): Array{
	return levelArray;
}
function GetLevelState(index : int):LevelState{
	
	var state = levelArray[index-1];
	
	return state;
}
function NewSave(){
	
	var data : PlayerData = new PlayerData();
	data.levelArray = levelArray;
	EasySerializer.SerializeObjectToFile (data, Application.persistentDataPath+"/data.dat");
}
function NewLoad(){
	var data : PlayerData = EasySerializer.DeserializeObjectFromFile (Application.persistentDataPath+"/data.dat");
	if(data != null){
		levelArray = data.levelArray;
		LevelsDone();
		OpenNext();
	}else
	{
		levelArray = new LevelState[20];
		
		for(var i = 0; i < levelArray.length; i++)
		{
			levelArray[i] = LevelState.NOT_OPEN;
		}
		NewSave();
		//Save();
		LevelsDone();
		OpenNext();
	}	
		
}
function Save(){
	var bf : BinaryFormatter = new BinaryFormatter();
	var file : FileStream = File.Create(Application.persistentDataPath+"/data.dat");
	
	var data : PlayerData = new PlayerData();
	data.levelArray = levelArray;
	bf.Serialize(file,data);
	file.Close();
}
function Load(){

	if(File.Exists(Application.persistentDataPath+"/data.dat"))
	{
		var bf : BinaryFormatter = new BinaryFormatter();
		var file : FileStream = File.Open(Application.persistentDataPath+"/data.dat",FileMode.Open);
		var data : PlayerData = bf.Deserialize(file);
		file.Close();
		levelArray = data.levelArray;
		LevelsDone();
		OpenNext();	
	}else
	{
		levelArray = new LevelState[20];
		
		for(var i = 0; i < levelArray.length; i++)
		{
			levelArray[i] = LevelState.NOT_OPEN;
		}
		NewSave();
		//Save();
		LevelsDone();
		OpenNext();
	}	
}

@Serializable
class PlayerData
{
	public var levelArray : LevelState[];
}