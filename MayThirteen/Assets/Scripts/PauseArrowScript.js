#pragma strict
private var ball : GameObject;
private var cam : GameObject;
private var speed:float =2;
function Awake(){
	if(Application.loadedLevelName == "LevelSelectionLobby"){
		Destroy(gameObject);
	}
}
function Start () {
	ball = GameObject.Find("Ball");
	cam = GameObject.Find("Camera2");
	transform.position = cam.transform.position;
	var myNewVector : Vector3 = Vector3((ball.transform.position.x)-cam.transform.position.x, (ball.transform.position.y+0.5)-cam.transform.position.y,ball.transform.position.z-cam.transform.position.z);
	myNewVector.Normalize();
	transform.position += myNewVector *18;	
}

function Update () {
	
	if(Time.timeScale == 1.0f || Application.loadedLevelName == "LevelSelectionLobby"){
		Destroy(gameObject);
	}
	
	//transform.position.x = ball.transform.position.x - 1.1f;	
	//transform.position.y = ball.transform.position.y + 6.5f;
	//transform.position.z = cam.transform.position.z +7.0f;
}
