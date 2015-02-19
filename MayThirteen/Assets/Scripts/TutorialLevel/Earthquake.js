#pragma strict
private var shake : boolean = false;
private var endShake : boolean = false;
private var shakeAmount : float = 0.4;
private static var myRunning : boolean= false;
private var camSci : CameraScript;
private var quakeEnded : boolean = false;
private var timer : float = 0.0f;
private var hasPlayed : boolean = false;
private var controller : ControllerScript;
private var pause : PauseScript;

function Awake(){
	camSci = Camera.main.gameObject.GetComponent("CameraScript");
	controller = Camera.main.gameObject.GetComponent("ControllerScript");
	pause = Camera.main.gameObject.GetComponent("PauseScript");
}
function Start () {
	if(camSci !=null){
		
		camSci.StartShake();
		audio.volume=1.0;
 		audio.Play();
 		hasPlayed= true;
	}
	pause.showGUI = false;
	
}
private var helper :boolean = false;
function Update () {
	if(!helper)controller.running=false;
	if(Time.timeScale == 0.0 && audio.isPlaying){
		audio.Pause();
	}else if(Time.timeScale == 1.0 && !audio.isPlaying && hasPlayed){
		audio.Play();
	}
	timer += Time.deltaTime;
	if(timer >8.0f && !helper){
		EndEarthQuake();
		helper= true;
	}
}
function Running(index : boolean){
		
		controller.running=index;
		
}
function FixedUpdate(){

}
function EndEarthQuake(){
		camSci.EndShake();
		pause.showGUI = true;
		FadeAudio(2.0, Fade.Out);
}
function FadeAudio (timer : float, fadeType : Fade) {
    var start = fadeType == Fade.In? 0.0 : 1.0;
    var end = fadeType == Fade.In? 1.0 : 0.0;
    var i = 0.0;
    var step = 1.0/timer;
 
    while (i <= 1.0) {
        i += step * Time.deltaTime;
        audio.volume = Mathf.Lerp(start, end, i);
        yield;
    }
}