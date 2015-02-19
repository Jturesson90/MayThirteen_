#pragma strict

private static var instance : MyUnitySingleton;
enum SongType {LOW,MIDDLE,HIGH};

public var backgroundMusic : AudioClip[];
private var currentClip : AudioClip;
private var audioSource : AudioSource;
private var x =0;
var fadeTime: float = 4f;
private var currentScene = "";
public var sample :float =0;
public var timeHaha : float = 0;
public static function GetInstance() : MyUnitySingleton {
    return instance;
}
 
function Awake() {
	
    if (instance != null && instance != this) {
        Destroy(this.gameObject);
        return;
    } else {
        instance = this;
    }
    
    
    DontDestroyOnLoad(this.gameObject);
}
function Start(){
	audioSource =  GetComponent(AudioSource);
	AudioListener.pause = PlayerPrefs.GetInt("SoundOn",1) == 1 ? false: true;
}
function OnApplicationFocus(){
	//AudioListener.pause = PlayerPrefs.GetInt("SoundOn",1) == 1 ? false : true;
}
function OnApplicationPause(){	
	//AudioListener.pause = true;
}	

function Update(){
	sample = audioSource.time;
	timeHaha += Time.deltaTime;
	if(Input.GetKeyDown(KeyCode.O)){
		//PlayRandomSong();
		changeSong();
		//audioSource.timeSamples += audioSource.clip.samples/audioSource.clip.length;
		print(audioSource.timeSamples+"\n"+audioSource.time+"\n");
		
	}
	if(currentScene != Application.loadedLevelName){
		currentScene = Application.loadedLevelName;
		CheckLevel();
	}
	
	
}
function FadeAudio (timer : float, fadeType : Fade, aC : AudioClip) {
 	
    var start = fadeType == Fade.In? 1.0 : 0.0;
 
    var end = fadeType == Fade.In? 0.0 : 1.0;
 
    var i = 0.0;
 
    var step = 1.0/timer;
   	
    while (i <= 0.95) {
 
        i += step * Time.deltaTime;
 
        audio.volume = Mathf.Lerp(start, end, i);
    yield;
	}
	if(i >= 0.95&& fadeType == Fade.In){
		audioSource.Play();
		//print("TJABBA");
		FadeAudio (fadeTime, fadeType.Out);
	}
}
function FadeAudio (timer : float, fadeType : Fade) {
 	
    var start = fadeType == Fade.In? 1.0 : 0.0;
 
    var end = fadeType == Fade.In? 0.0 : 1.0;
 
    var i = 0.0;
 
    var step = 1.0/timer;
   
    while (i <= 0.95) {
 
        i += step * Time.deltaTime;
 
        audio.volume = Mathf.Lerp(start, end, i);
    yield;
 }
}

function CheckLevel(){
	switch (Application.loadedLevelName){
		
		case  "LevelX" :
			RandomSongType(SongType.LOW);	
		break;
		
		case "Menu":
			PlaySong(0);
		break;
		
		case "LevelSelectionLobby":
			var levelCR = PlayerPrefs.GetInt("LevelsDone");
			if(levelCR>9){
				//RandomSongType(SongType.HIGH);
				PlaySong(9);
			}else if(levelCR>8){
				PlaySong(8);
			}else if(levelCR>4){
				
				RandomSongType(SongType.MIDDLE);
			}else {
				RandomSongType(SongType.LOW);
			}
		break;
		case "Splash" :break;
		default :
		//	print("LEVEL");
			var levelNR = PlayerPrefs.GetInt("CurrentLevel");
			if(levelNR>9){
				//RandomSongType(SongType.HIGH);
				PlaySong(9);
			}else if(levelNR>8){
				PlaySong(8);
			}
			else if(levelNR>4){
				
				RandomSongType(SongType.MIDDLE);
			}else {
				RandomSongType(SongType.LOW);
			}
			
		break;
	}
	
}
function PlaySong(id : int){
	//print("Song.id : "+id+ "  "+ backgroundMusic[id]);
	var timeInSamples = audioSource.time;
	var timeSince = Time.time;
	audioSource.clip = backgroundMusic[id];
	
	if(audioSource.clip != currentClip){
		
		currentClip = audioSource.clip;
		
		//FadeAudio(fadeTime, Fade.In,currentClip);
		audioSource.Play();
		timeSince = Time.time - timeSince;
		print(timeSince);
		audioSource.time= timeInSamples+timeSince;
	}
}
function changeSong(){
	if(audioSource.clip == backgroundMusic[8] )
		PlaySong(backgroundMusic.Length-1);
		else 
			PlaySong(backgroundMusic.Length-2);
	return;
}
function RandomSongType(type : SongType){
	switch(type){
		case SongType.LOW :
			PlaySong(Random.Range(0, 3));
			
		break;
		
		case SongType.MIDDLE: 
			PlaySong(Random.Range(4,7));
			
		break;
		
		case SongType.HIGH :
			
			PlaySong(Random.Range(8, backgroundMusic.Length));
			
		break;
		default:
		break;
	}
}
function PlayRandomSong(){
	audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
	audioSource.Play();
}