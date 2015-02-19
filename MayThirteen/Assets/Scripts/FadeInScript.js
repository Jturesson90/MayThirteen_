#pragma strict
var fadeSpeed =1.5f;
private var sceneStarting : boolean= true;
var fadeOut:boolean=false;
private var levelString : String ="Menu";
private var savedHeight: float;
private var newLevel = false;

function Awake(){
	
	guiTexture.pixelInset = new Rect(0f,0f,Screen.width,Screen.height);
	savedHeight = Screen.height;
	Time.timeScale=1;
}
function Start(){

}
function Update(){
	
	
	if(Screen.height != savedHeight)
		guiTexture.pixelInset = new Rect(0f,0f,Screen.width,Screen.height);
	
	if(fadeOut){
		FadeToBlack();
	}else {
		FadeToClear();
	}
}
function FadeToLevel(str : String){
	fadeOut = true;
	newLevel = true;
	guiTexture.color.a = 0f;
	levelString = str;
}
function FadeToClear(){
	if(guiTexture.color.a < 0.05f){
		guiTexture.color.a = 0f;
		return;
	}
	guiTexture.color = Color.Lerp(guiTexture.color,Color.clear,fadeSpeed*Time.deltaTime);
}
function FadeToBlack(){
	
	guiTexture.color = Color.Lerp(guiTexture.color,Color.black,fadeSpeed*Time.deltaTime);
	
	if(guiTexture.color.a > 0.95f && newLevel){
		fadeOut=false;
		Application.LoadLevel(levelString);
		newLevel = false;
	}
}