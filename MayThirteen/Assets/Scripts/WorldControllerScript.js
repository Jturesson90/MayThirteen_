#pragma strict
var camera1: Camera;
var camera2 : Camera;
private var cameraHelper : boolean = false;
function Start () {
	camera1.enabled=true;
	camera2.enabled = false;
	
}

function Update () {
	if(Application.loadedLevelName=="LevelX")return;
	if(Time.timeScale == 0.0f && !cameraHelper){
		camera2.enabled = true;
		camera1.enabled = false;
		camera2.tag = "MainCamera";
		camera1.tag = "Camera2";
		cameraHelper = true;
	}else if (Time.timeScale > 0.1f && cameraHelper){
		camera2.enabled = false;
		camera1.enabled = true;
		cameraHelper = false;
		camera1.tag = "MainCamera";
		camera2.tag = "Camera2";
	}
}