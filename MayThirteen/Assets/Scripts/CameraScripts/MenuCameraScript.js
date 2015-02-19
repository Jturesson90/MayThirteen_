#pragma strict
private var ball : GameObject;
private var turnSpeed : float = 5f;
private var width : float = 1920f;
private var height : float = 1080f;
var dSize :float = 1f;

var startButtonSkin : GUISkin;
var buttonSize :float = 100f;
private var buttonHeight : float =100f;
private var buttonWidth : float =100f;
//private var fade : FadeInScript;
private var showGUI : boolean= true;
private var test =	false;
private var	inapp : InApp;
var noAdsSize : float = 50;
var nAX: float = 50;
var nAY: float = 50;
private var fadeText:FadingText;
public var noAds : GUISkin;
private var buttonsSize: float = 200.0f;
private var showQuit = false;
var guiColor : Color32;
var timerHelper = false;
private var timer : float=0f;
private var inappLabel: String = "";
function Awake(){
	//fade = GameObject.Find("FadeInObject").GetComponent("FadeInScript");
	fadeText = GetComponent("FadingText");
	ball = GameObject.FindGameObjectWithTag("Ball").gameObject;
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	if(Application.isEditor == false)inapp = GameObject.Find("InApp").GetComponent("InApp");
}
function Start () {
	showGUI = false;

}

function Update () {
	timer += Time.deltaTime;
	if(timer > 2f&& !timerHelper && !showQuit){
		showGUI = true;
		timerHelper=true;
		
	}
	transform.position.x = ball.transform.position.x;
	transform.position.y = ball.transform.position.y;
	Physics2D.gravity = -transform.up*9.81;
	if(inapp !=null){
		inappLabel=inapp.GetLabel();
	 	if( inappLabel != ""){
	 		fadeText.Text(inappLabel,8);
	 	}
	}
}
function FixedUpdate(){
	transform.Rotate(0, 0, Time.deltaTime*turnSpeed);

}


		
		

		