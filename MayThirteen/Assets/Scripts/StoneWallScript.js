#pragma strict
private var cameraScript : CameraScript;
var wallIndex : int;
enum Fade {In, Out}
private var hasPlayed : boolean = false;



function Awake(){

	//cameraScript = GameObject.Find("Camera").GetComponent("CameraScript");
}
function Start () {
	
	
	
//	print("LevelsDone: "+ PlayerPrefs.GetInt("LevelsDone",0));
	//StoneWallFunction(PlayerPrefs.GetInt("LevelsDone"));
}

function Update () {
	if(Time.timeScale == 0.0 && GetComponent.<AudioSource>().isPlaying){
		GetComponent.<AudioSource>().Pause();
	}else if(Time.timeScale == 1.0 && !GetComponent.<AudioSource>().isPlaying && hasPlayed){
		GetComponent.<AudioSource>().Play();
	}
	
}
function OpenStoneWall(index : int){
	for(var stone : GameObject in GameObject.FindGameObjectsWithTag("StoneWall"))
		{
			StartAnimationWithDelay(index,0f);	
		}
}
function StoneWallFunction(number : int){
	if(number% 5 == 0 && number != 0){
		if(GameObject.Find("StoneWall") != null){
			for(var stone : GameObject in GameObject.FindGameObjectsWithTag("StoneWall"))
			{
				var stoneScript : StoneWallScript = stone.GetComponent("StoneWallScript");
				stoneScript.StartAnimationWithDelay(number,2f);	
			}
		}
	}
}
function StartAnimationWithDelay(index : int, timer: float){
	//print(index+"\t"+wallIndex);
	if(wallIndex == index){
		yield WaitForSeconds(timer);
		transform.GetComponent.<Animation>().Play("SlideUp");
	}
}
function StartAnimation(){
 	cameraScript.StartShake();
 	GetComponent.<AudioSource>().volume=1.0;
 	GetComponent.<AudioSource>().Play();
 	hasPlayed= true;
}
function AtEndOfAnimation(){
	cameraScript.EndShake();
	
	
	FadeAudio(2.0, Fade.Out);
}

function FadeAudio (timer : float, fadeType : Fade) {
    var start = fadeType == Fade.In? 0.0 : 1.0;
    var end = fadeType == Fade.In? 1.0 : 0.0;
    var i = 0.0;
    var step = 1.0/timer;
 
    while (i <= 1.0) {
        i += step * Time.deltaTime;
        GetComponent.<AudioSource>().volume = Mathf.Lerp(start, end, i);
        yield;
    }
    Destroy(gameObject);
}