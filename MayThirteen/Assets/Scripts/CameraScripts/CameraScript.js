#pragma strict

var objectTarget : Transform;
var followPlayer : boolean = true;
private var controllerScript : ControllerScript;
private var pauseScript : PauseScript;
var myFont : GUIStyle;
public var range : int =0;
var star : Texture;
public var starTime : float =50.0;
private var nativeVerticalResolution = 1080.0;
private var nativeHorizontalResolution = 1920.0;
private var won : boolean = false;
private var smoothnes = 2.0;
private var cameraFieldTarget :float  = 90;
private var cameraWonTarget = Quaternion.Euler (0, 0, 0);
private var wantToZoom :boolean =true;
private var doneYeildOneTime:boolean = false;
private var endShake :boolean = false;
var shake : boolean = false;
private var shakeAmount : float = 0.4;
private var haveWon = false;
private var stopZoomOnce : boolean = true;
private var extraSpeed : float = 1.0f;
var decreaseFactor : float = 1.0;

public var x =0;
public var y = 0;
public var w = 0;
var myColor: Color32;
var myInnerColor : Color32;
var h = 0;
var size = 2;


private var didBeatStarTime : boolean = false;
function Awake(){
	won = false;
	
	pauseScript = GameObject.Find("Camera").GetComponent("PauseScript");
	controllerScript = GameObject.Find("Camera").GetComponent("ControllerScript");
}

var helperOneTime=false;
private var checkRange :float;
private var orthoHelperOneTime :boolean = false;
private var speed:float;

function ZoomToTarget(){
	if(!doneYeildOneTime && !helperOneTime){
		helperOneTime=true;
		yield WaitForSeconds(1.5f);
		doneYeildOneTime = true;	
	}
	if(!doneYeildOneTime)return;	
	var range : float = Vector3.Distance( transform.position,Vector3(objectTarget.transform.position.x,objectTarget.transform.position.y,-10));
 	speed = range;
 	if(range < 3){
    	speed=3;
    }
 	if((camera.orthographicSize >=10) && camera.isOrthoGraphic){
 	 	if(!orthoHelperOneTime){
	        camera.orthographicSize -= extraSpeed * speed * Time.deltaTime;
        	
		}
 	}else if(camera.orthographicSize <= 10){
        orthoHelperOneTime = true;
		camera.orthographicSize= 10;
	}
 	checkRange = extraSpeed > 1.0 ? 0.3f : 0.1f; 
   	if (range > checkRange){
        var dir: Vector3 = Vector3(objectTarget.transform.position.x,objectTarget.transform.position.y,-10)- transform.position;
        dir = dir.normalized;
        transform.Translate(extraSpeed * dir * speed * Time.deltaTime, Space.World);
   }
   else if(orthoHelperOneTime || !camera.isOrthoGraphic){
       wantToZoom=false;
       camera.orthographicSize= 10;
       camera.transform.position.z = -10;
	   controllerScript.running = true;
    }
   
}

function Update () {
	if(controllerScript.stopZoom  && stopZoomOnce){
		extraSpeed = 6.0f;
	}
	if(wantToZoom){
		//ZoomToPlayer();
		ZoomToTarget();
	}else {
		if(followPlayer){
			transform.position.x = objectTarget.transform.position.x;
			transform.position.y = objectTarget.transform.position.y;
		}
	}
	if(won){	
		transform.rotation =  Quaternion.Lerp(transform.rotation,cameraWonTarget, Time.deltaTime * 2.0);
	}
	if(!controllerScript.useAccelerometer){
		Physics2D.gravity = -transform.up*9.81;
	}
	else if(controllerScript.useAccelerometer){
		transform.rotation.x=0;
		transform.rotation.y=0;
		transform.rotation.z=0;
	}
	Shake();	
}

function StopFollow(canBeDead: boolean){
	followPlayer = false;
	if(!canBeDead)return;
	controllerScript.running = false;	
	yield WaitForSeconds(1);
	pauseScript.SetTryAgain();
	//controllerScript.Restart();
}
var currentLevel: int;
function Start(){
	currentLevel = PlayerPrefs.GetInt("CurrentLevel");
} 
function Goal(){
	if(won)return;
	won = true;
	controllerScript.running = false;
	yield WaitForSeconds(1);
	if(TimerScript.starTimer < starTime){
		LevelHandler.handler.UpdateArray(currentLevel, LevelState.DONE_STAR);
		didBeatStarTime = true;
	}else{
		LevelHandler.handler.UpdateArray(currentLevel, LevelState.DONE);
	}
	
	
	pauseScript.SetWon();
	
}
function Vibrate(){
#if UNITY_ANDROID || UNITY_IPHONE
	Handheld.Vibrate();
#endif
}
function Shake(){
	 if (shake && Time.timeScale == 1.0) {
	    transform.localPosition +=Random.insideUnitCircle * shakeAmount;
//	    shake -= Time.deltaTime * decreaseFactor;
	 	Vibrate();
	 	
	  }
	  else if(endShake && Time.timeScale == 1.0){
	 	 transform.localPosition += Random.insideUnitCircle*shakeAmount;
	 	 shakeAmount *= 0.9f;
	 	Vibrate();
	 
	 	 if(shakeAmount < 0.05f){
	 	 	endShake = false;
	 	 }
	  }
}

function StartShake(){
	shake= true;
	endShake = false;
	shakeAmount = 0.4;
}

function EndShake(){
	shake = false;
	endShake = true;
}
function OnGUI(){
 	GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1));
	//if(Application.loadedLevelName=="LevelX")return;
	if(won){
		myFont.fontSize=90;
		myFont.normal.textColor = Color32(0,0,0,255);
		myFont.alignment = TextAnchor.MiddleCenter;
		//GUI.Label(new Rect(nativeHorizontalResolution/2,nativeVerticalResolution/2, 0, 0), "You have reached the goal!", myFont);
		ShadowAndOutline.DrawOutline( Rect (nativeHorizontalResolution/2,nativeVerticalResolution/2, w, h), "Level completed!", myFont, myColor, myInnerColor, size );
		
		if(Application.loadedLevelName != "LevelX"){
			if(didBeatStarTime){
				GUI.DrawTexture(Rect(170,10,star.width,star.height), star);
				myFont.fontSize = 70;
				myFont.alignment = TextAnchor.MiddleLeft;
				myFont.normal.textColor = Color32(230,230,230,255);
				ShadowAndOutline.DrawOutline( Rect (x, y, w, h), "You got a Superstar!", myFont, myColor, myInnerColor, size );
				//GUI.Label(new Rect(nativeHorizontalResolution/2,nativeVerticalResolution/2+75, 0, 0), "You got a star!", myFont);
			}else{
				myFont.fontSize = 70;
				myFont.alignment = TextAnchor.MiddleLeft;
				myFont.normal.textColor = Color32(230,230,230,255);
				//GUI.Label(Rect(140,210, 0, 0), , myFont);
				ShadowAndOutline.DrawOutline( Rect (x, y, w, h), "Superstar time: "+starTime, myFont, myColor, myInnerColor, size );
			}
		}
	}
}