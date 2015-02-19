#pragma strict
private var nativeVerticalResolution = 1080.0;
private var nativeHorizontalResolution = 1920.0;
private var controllerScript : ControllerScript;
var mySprite : Texture;
var myFont : GUIStyle;
private var myTimer : float;
private var spriteWidth:float;
private var spriteHeight:float;

public static var starTimer : float=0;
function Start () {
	myTimer =0.0;
	controllerScript = GameObject.Find("Camera").GetComponent("ControllerScript");
		spriteWidth =  mySprite.width *0.9f;
		spriteHeight  = mySprite.height *0.9f;
		
}

function Update () {
	if(controllerScript.playerGO && controllerScript.running){
		myTimer += Time.deltaTime;
	}
	starTimer = myTimer;
}

function OnGUI(){
		if(Application.loadedLevelName == "LevelSelectionLobby"||Application.loadedLevelName == "LevelX")return;
		GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1)); 
		GUI.DrawTexture(Rect(140,10,spriteWidth,spriteHeight), mySprite);
		GUI.Label(new Rect(140+spriteWidth/2,10 +spriteHeight/2, 0, 0), ""+myTimer.ToString("F2"), myFont);
		
		
}