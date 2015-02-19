#pragma strict

var guiSkin: GUISkin;
var backToMenuSkin : GUISkin;
var restartSkin : GUISkin;
var playSkin : GUISkin;
var soundSkin : GUISkin;
var soundOffSkin : GUISkin;
var menuSkin : GUISkin;
var noAds : GUISkin;
var myFont : GUIStyle;
var showGUI : boolean = true;
var smallButtonsSize: float = 160;
private var nativeVerticalResolution = 1080.0;
private var nativeHorizontalResolution = 1920.0;
var isPaused : boolean = false;
var aTexture : Texture;
var starTexture : Texture;
var haveWon = false;
private var hej=false;
private var fade:FadingText;
var pauseArrows : Transform;
private var tryAgain = false;
private var wonButtonSize = 300.0;
private var	inapp : InApp;
private var controller : ControllerScript;
var guiColor : Color32;
var outerColor: Color32;
var innerColor : Color32;
private var inappLabel: String = "";

function Awake(){
	isPaused = false;
	Time.timeScale = 1.0f;
	controller = GameObject.Find("Camera").GetComponent("ControllerScript");
	if(Application.isEditor == false)inapp = GameObject.Find("InApp").GetComponent("InApp");
	fade = GetComponent("FadingText");
	//KEEP AWAKE
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
}


private var arrowHelper : boolean = false;
function Update()
{ 	
 	if(Time.timeScale == 0.0 && !arrowHelper){	 
 		 arrowHelper=true;
 		 Instantiate(pauseArrows);
 	}else if (Time.timeScale ==1.0 &&arrowHelper){ 
 		arrowHelper= false;
 	}
 	if(inapp !=null){
		inappLabel = inapp.GetLabel();
	 	if( inappLabel != ""){
	 		fade.Text(inappLabel,8);
	 	}
	 }
}
function SetWon(){
	haveWon = true;
}
function SetTryAgain(){
	tryAgain = true;
}
function MakeAButton(size:float,x:float,y:float, theSkin : GUISkin){
	if(PlayerPrefs.GetInt("NoAds",0)!=1 && Application.loadedLevelName == "Menu"){
	   GUI.skin = noAds;
      		if(GUI.Button(Rect(nativeHorizontalResolution/2,nativeVerticalResolution,120,120),"")){
      			print("No ADS!");
      			//inapp.BuyNoAds();
      		}
      	}
}
function OnGUI ()
{
	GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1)); 
 	if(!showGUI)return;
 	
 
 	//Stars
 /*	if(isPaused && Application.loadedLevelName == "LevelSelectionLobby")
 	{
 	//	GUI.DrawTexture(Rect(10,nativeVerticalResolution-90,80,80),starTexture);
 	//	myFont.fontSize=85;
 		//GUI.Label(new Rect(90,nativeVerticalResolution,0,0),PlayerPrefs.GetInt("Stars",0)+"/20",myFont);
 	//	ShadowAndOutline.DrawOutline( Rect (90,nativeVerticalResolution, 0, 0), PlayerPrefs.GetInt("Stars",0)+"/20", myFont, outerColor,  innerColor,5.0f );
				
 	}
 */	
 	GUI.color = guiColor;
 	if(haveWon){
 		//RestartButton
 		GUI.skin = restartSkin;
    	if(Application.loadedLevelName != "LevelSelectionLobby"){
		   	if(GUI.Button(Rect(nativeHorizontalResolution/2-wonButtonSize-wonButtonSize*0.5,nativeVerticalResolution*0.75-wonButtonSize*0.5,wonButtonSize,wonButtonSize),"")){
				//print("CLICKING Restart!");
				Application.LoadLevel (Application.loadedLevelName);
			}
		if(PlayerPrefs.GetInt("NoAds",0)!=1){
				GUI.skin = noAds;
		      	if(GUI.Button(Rect(nativeHorizontalResolution*0.85-200/2,550,200,200),"")){
					//print("No ADS!");
					if(inapp !=null){
						inapp.BuyNoAds();
					}
		      	}
		     }
        }
	    //BackToMenuButton
 		GUI.skin = backToMenuSkin;
 		if(GUI.Button(Rect(nativeHorizontalResolution/2+wonButtonSize*0.5,nativeVerticalResolution*0.75 - wonButtonSize*0.5 ,wonButtonSize,wonButtonSize),"")){
			//print("CLICKING BackToMenu");
			//Time.timeScale = isPaused ? 1.0f : 0.0f;
			Application.LoadLevel("LevelSelectionLobby");
		}
	
 	}else if(tryAgain)
 	{
 		GUI.skin = restartSkin;
    	if(Application.loadedLevelName != "LevelSelectionLobby"){
		   	if(GUI.Button(Rect(nativeHorizontalResolution/2-wonButtonSize*0.5,nativeVerticalResolution*0.75-wonButtonSize*0.5,wonButtonSize,wonButtonSize),"")){
				//print("CLICKING Restart!");
				Application.LoadLevel (Application.loadedLevelName);
			}
			if(PlayerPrefs.GetInt("NoAds",0)!=1){
				GUI.skin = noAds;
		      	if(GUI.Button(Rect(nativeHorizontalResolution*0.75-200/2,550,200,200),"")){
					//print("No ADS!");
					if(inapp !=null){
						inapp.BuyNoAds();
					}
		      	}
		     }
        }
        	
 	}
 	else
 	{
	 	isPaused = Time.timeScale == 0.0f ? true : false;
	/*   GUI.skin = isPaused ? playSkin : guiSkin;
		if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize-10,5,smallButtonsSize,smallButtonsSize),""))
		{
			//print("CLICKING PAUSE");
			Time.timeScale = isPaused ? 1.0f : 0.0f;
		}*/
		GUI.skin = backToMenuSkin;
	 	if(isPaused)
	    {
	    	
	     	if(Application.loadedLevelName != "LevelSelectionLobby" && Application.loadedLevelName != "LevelX"){
	     		myFont.fontSize=100;
	     		
	     		GUI.color = Color.white;
				//GUI.Label(new Rect(10,nativeVerticalResolution, 0, 0), "Level "+ PlayerPrefs.GetInt("CurrentLevel"), myFont);
				ShadowAndOutline.DrawOutline( Rect (10,nativeVerticalResolution, 0, 0), "Level "+ PlayerPrefs.GetInt("CurrentLevel"), myFont, outerColor,  innerColor,5.0f );
				GUI.color = guiColor;
				if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize * 2- 10*2,5,smallButtonsSize,smallButtonsSize),"")){
					//print("CLICKING BackToMenu");
					Application.LoadLevel("LevelSelectionLobby");
				}
				GUI.skin = restartSkin;
				if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize*3-10*3,5,smallButtonsSize,smallButtonsSize),"")){
					//print("CLICKING Restart!");
					Application.LoadLevel (Application.loadedLevelName);
				}
	      	}/*else if(Application.loadedLevelName == "LevelSelectionLobby" || Application.loadedLevelName == "LevelX"){
	      		GUI.skin = menuSkin;
	      		if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize*2-10*2,5,smallButtonsSize,smallButtonsSize),"")){
					//print("CLICKING BackToMenu");
					Application.LoadLevel("Menu");
				}
	      	}*/
	      	
	      	if(PlayerPrefs.GetInt("NoAds",0)!=1 && Application.loadedLevelName != "LevelX"){
		      	GUI.skin = noAds;
		      	
		      	if(Application.loadedLevelName == "LevelSelectionLobby"){
		      		if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize*3-10*3,5,smallButtonsSize,smallButtonsSize),"")){
		      			//print("No ADS!");
		      			inapp.BuyNoAds();
		      		}
		      	}else if(Application.loadedLevelName != "Menu" || Application.loadedLevelName != "Splash"){
		      		if(GUI.Button(Rect(nativeHorizontalResolution-smallButtonsSize*4-10*4,5,smallButtonsSize,smallButtonsSize),"")){
		      			//print("No ADS!");
		      			inapp.BuyNoAds();
		      		}
		      	}
	      	}
	      	
	      /*	GUI.skin = !AudioListener.pause ? soundSkin : soundOffSkin;
	      	if(GUI.Button(Rect(10,5,120,120),"")){
	      			
					AudioListener.pause = AudioListener.pause ? false: true;
					var soundOn : int = AudioListener.pause ? 0: 1;
					PlayerPrefs.SetInt("SoundOn",soundOn);
					Debug.Log("SoundON "+PlayerPrefs.GetInt("SoundOn",1));
					
				}
	     */
	   }
	   
	   /* ARROWS
	   if(!isPaused && controller.running){
			GUI.DrawTexture(Rect(10,nativeVerticalResolution/2+100,200,-200), aTexture);
			GUI.DrawTexture(Rect(nativeHorizontalResolution-10,nativeVerticalResolution/2+100,-200,-200), aTexture);
	   } */
 }
 
}