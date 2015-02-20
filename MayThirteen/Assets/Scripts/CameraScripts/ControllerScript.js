#pragma strict
var camera2: Camera;
private var turnSpeed :float =55f; 
var playerGO : boolean = false;
private var haveWon = false;
var stopZoom : boolean = false;

public var running : boolean = false;
function Awake(){

}
function Start () {
	stopZoom = false;

}
function FixedUpdate(){
	if(!running){
		return;
	}

}

function Update () {
	if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began  ){
		stopZoom =true;
	}
	if(Input.GetKey(KeyCode.L)){
		stopZoom =true;
	}
	if(!running){
		return;
	}
	

	if(Input.touchCount ==2)return;
		for (var i = 0; i < Input.touchCount; ++i) {		
			if(Input.GetTouch(i).position.x < Screen.width/4 ){
				transform.Rotate(0, 0, -Time.deltaTime*turnSpeed);
				if(!playerGO) playerGO=true;
			}
			if(Input.GetTouch(i).position.x > Screen.width*0.75 ){
				transform.Rotate(0, 0, Time.deltaTime*turnSpeed);
				if(!playerGO) playerGO=true;
			}
	}
	
	if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(0, 0, -Time.deltaTime*turnSpeed);
			if(!playerGO) playerGO=true;
	}
	if(Input.GetKey(KeyCode.RightArrow)){
		transform.Rotate(0, 0, Time.deltaTime*turnSpeed);
			if(!playerGO) playerGO=true;
	}
}
