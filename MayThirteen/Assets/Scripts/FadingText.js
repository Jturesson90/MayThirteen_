#pragma strict

private var timer : float=0f;
private var fadeSpeed : float = 3f;
private var isActive : boolean = false;
private var myText : String = "";
var outerColor : Color;
var innerColor : Color;
var myFont : GUIStyle;
private var width = 1920.0f;
private var height = 1080.0f;
private var maxTime:float = 4f;
private var fadeIn :boolean =false;


function Start(){
	outerColor.a = 0f;
	innerColor.a = 0f;
}
function Update () {
		if(!isActive){
			outerColor.a = 0f;
			innerColor.a = 0f;
		}
		if(isActive && timer < maxTime){
			timer += Time.deltaTime;
			FadeIn();
		}else if(isActive){
			FadeOut();
		}
}
function Text(str : String){
		maxTime = 4f;
		isActive = true;
		timer = 0f;
		myText = str;
}
function Text(str : String, mT :int){
		maxTime = mT;
		isActive = true;
		timer = 0f;
		myText = str;
}
function OnGUI(){
	GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / width, Screen.height / height, 1));
	if(isActive){
		ShadowAndOutline.DrawOutline( Rect (width/2,height*0.3, 0, 0), myText, myFont,outerColor, innerColor,5f);
	}
}

function FadeOut(){
	
	outerColor.a = Mathf.Lerp(outerColor.a, 0f, fadeSpeed*Time.deltaTime);
	innerColor.a = Mathf.Lerp(innerColor.a, 0f, fadeSpeed*Time.deltaTime);
	if(outerColor.a < 0.05f){
		isActive = false;
	}
}
function FadeIn(){
	
	innerColor.a = Mathf.Lerp(innerColor.a, 1f, fadeSpeed*Time.deltaTime);
	outerColor.a = Mathf.Lerp(outerColor.a, 1f, fadeSpeed*Time.deltaTime);
}
