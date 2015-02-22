#pragma strict
private var cameraScript : CameraScript;
var brokenStone : GameObject;
private var canPressLevel : boolean;
private var explosionPos : Vector3;
var myFont : GUIStyle;
private var nativeVerticalResolution = 1080.0;
private var nativeHorizontalResolution = 1920.0;
private var dead = false;
private var canBeDead =true;
private var rend : SpriteRenderer;
var outerColor: Color;
var innerColor : Color;

function Awake () {
	cameraScript = Camera.main.GetComponent("CameraScript");
	}
function Start(){

	rend = gameObject.GetComponent("SpriteRenderer");
	if(PlayerPrefs.HasKey("PlayerXPos")&&PlayerPrefs.HasKey("PlayerYPos")&&Application.loadedLevelName == "LevelSelectionLobby"){
		gameObject.transform.position.x = PlayerPrefs.GetFloat("PlayerXPos");
		gameObject.transform.position.y = PlayerPrefs.GetFloat("PlayerYPos");
		
	}
}

function Update () {
}
function OnTriggerEnter2D(hit :  Collider2D){
	
	if(renderer.enabled == false){
		return;
	}
	if(hit.gameObject.tag=="OutOfBounds"){	
		cameraScript.StopFollow(canBeDead);
		if(canBeDead){
			dead = true;
		}
	}
	if(hit.gameObject.tag == "Goal"){
		cameraScript.Goal();
		canBeDead = false;
	}

		
}
function OnTriggerExit2D(hit : Collider2D){
	if(hit.gameObject.tag == "ALevel"){
		canPressLevel = false;
	}
}


function LoadNextLevel(level : String){
	if(Application.loadedLevelName == "LevelSelectionLobby"){
		PlayerPrefs.SetFloat("PlayerXPos",gameObject.transform.position.x);
		PlayerPrefs.SetFloat("PlayerYPos",gameObject.transform.position.y);
	}
	Application.LoadLevel(level);
}
function OnDestroy() {
	
}
function Die(){
	
	rend.enabled = false;
	var halo : Component;
	halo = GetComponent("Halo"); 
	halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	cameraScript.StopFollow(canBeDead);
	rigidbody2D.isKinematic = true;
	dead = true;
	collider2D.enabled = false;
	instantiateBrokenStone();
	
}
function instantiateBrokenStone(){
 	Instantiate(brokenStone, Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
}

function OnGUI(){
	GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1)); 
 	if(canBeDead && dead){
 		ShadowAndOutline.DrawOutline( Rect (nativeHorizontalResolution/2,nativeVerticalResolution/2, 0, 0), "Try again!", myFont,  innerColor, outerColor,12.0f );
 		//GUI.Label(new Rect(nativeHorizontalResolution/2,nativeVerticalResolution/2, 0, 0), "Try again!", myFont);
	}
}