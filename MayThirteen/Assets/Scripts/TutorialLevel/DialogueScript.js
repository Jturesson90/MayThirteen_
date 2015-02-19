#pragma strict
var width = 1920.0f;
var height = 1080.0f;
var myFont : GUIStyle;
var dialogKeeper : int;
var innerColor : Color32;
var outerColor : Color32;
var speed : float = 3.0f;
private var earth : Earthquake;
private var newArrayIncoming : boolean = false;
private var newGUIColor : Color;
var size : float = 5f;
private var firstTimeCheck = false;
private var showPressScreen : boolean = true;
private var fadeInMyText = false;
var dialogArray = new Array(
"Hello.. Are you there?",
"You're far away from home, Little Rockstar..",
"You shook loose from the mountain \nin the earthquake..",
"Come on, you have to get home..",
"Move Little Rockstar!",
"Oh, you can't move..",
"I'll give you the power to change Gravity..",
"Press < to alter Gravity clockwise \nand > to alter counterclockwise",
"");
private var canPress : float = 0f;
var dialogString : String ="";
private var fadeIn=false;
private var fade = false;
private var firstPress : boolean = true;
function Awake(){
	earth = GameObject.Find("Earthquake").GetComponent("Earthquake");
	Running(false);
}
function Start () {
	dialogKeeper = 0;
	newGUIColor = Color.white;
	newGUIColor.a = 0;
}
private var colorHelperReloaded = true;
private var fadeOut :boolean = false;
function Update () {
	canPress += Time.deltaTime;
	if(canPress> 9f){
		firstTimeCheck = true;
	}
	if(Input.GetMouseButtonDown(0)&& canPress>1f&&firstTimeCheck && dialogKeeper < dialogArray.length && dialogKeeper != 7){
		NextDialog(dialogKeeper);
		canPress = 0.0f;
	}else if(canPress>4f && firstTimeCheck && dialogKeeper < dialogArray.length ){
		NextDialog(dialogKeeper);
		canPress = 0.0f;
	}
	if(fadeIn){
		FadeIn();
	}else if(fadeInMyText){
		FadeIn();
	}else if(fadeOut){
		FadeOut();
	}
	if(dialogKeeper >6){
		Running(true);
	}else if(!newArrayIncoming){
		Running(false);
	}
	if(newGUIColor.a <= 0.05f&&colorHelperReloaded){
		newGUIColor.a = 0f;
		if(dialogKeeper < dialogArray.length){
			if(dialogKeeper <0)dialogKeeper = 0;
			dialogString = dialogArray[dialogKeeper];
			print("DialogKeeper: "+dialogKeeper+"\narrayLength: " +dialogArray.length);	
		}
		colorHelperReloaded = false;
		if(!firstPress){
			fadeIn=true;
		}
	}else if(newGUIColor.a >= 0.95f){
		colorHelperReloaded = true;
		if(firstPress)firstPress=false;
		//if(dialogKeeper == 7)Running(true);
	}
	if(fadeInMyText){
		FadeIn();
	}
}
function NewDialogArray(arr : Array){
	dialogArray = null;
	canPress = 10f;
	newArrayIncoming = true;
	firstPress = true;
	fadeInMyText = false;
	dialogKeeper = -1;
	dialogArray = new Array(arr);
}
function Running(bool : boolean){
	earth.Running(bool);
}
function NextDialog(index : int){
	
	if(index > dialogArray.length)return;
	//print("arr: length: " + dialogArray.length);
	//print("index: " +index +"\tarr: "+ dialogArray[index]);
	if(fadeInMyText)return;
	if(firstPress){
		fadeIn=true;
		fadeOut=false;
		
	}else if(newGUIColor.a >= 0.95f){
		fadeIn=false;
		fadeOut=true;
		dialogKeeper++;
		
	}
	
	
}
function FadeInText(myText : String){
	firstPress = true;
	fadeInMyText = true;
	dialogString = myText;
	canPress = 0.0f;
}
function FadeIn(){
	newGUIColor = Color.Lerp (newGUIColor, new Color (newGUIColor.r, newGUIColor.g,newGUIColor.b, 1), speed * Time.deltaTime);
}
function FadeOut(){
	newGUIColor = Color.Lerp (newGUIColor, new Color (newGUIColor.r, newGUIColor.g,newGUIColor.b, 0), speed * Time.deltaTime);
}
function OnGUI(){
	GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / width, Screen.height / height, 1)); 
	innerColor = Color(innerColor.r,innerColor.g,innerColor.b,newGUIColor.a);
	outerColor.a = innerColor.a;
	ShadowAndOutline.DrawOutline( Rect (width/2,height*0.3, 0, 0), dialogString, myFont,outerColor, innerColor,size);
	

}