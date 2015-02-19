#pragma strict

private var halo : Component;
var selectedLevel: int;
var clickable :boolean;
private var cam : Camera;
var outerColor : Color32;
var innerColor : Color32;
private var nativeVerticalResolution = 1080.0;
private var nativeHorizontalResolution = 1920.0;
private var ballScript : BallScript;
private var isOpen : boolean;
var myFont : GUIStyle;
private var state :LevelState;

function Awake(){
	
	halo = GetComponent("Halo"); 
	halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	ballScript = GameObject.Find("Ball").GetComponent("BallScript");
	 
}
function Start () {
	SetStar(false);
	CheckOpen();
	
}
function CheckOpen(){
		state = LevelHandler.handler.GetLevelState(selectedLevel);
		switch(state){
			case LevelState.OPEN:
				isOpen = true;	
			break;
			case LevelState.NOT_OPEN:
				isOpen = false;
				SetNotOpen();
			break;
			case LevelState.DONE:
				isOpen = true;
				SetDoneIcon();
			break;
			case LevelState.DONE_STAR:
				SetDoneIcon();
				isOpen = true;	
				SetStar(true);
			break;
			default:
			
			break;
		}
	
}
function SetNotOpen(){

	renderer.material.color = Color(0.3,0.3,0.3);	
}
function SetStar(arg : boolean){
	transform.FindChild("star").gameObject.SetActive(arg);
}
function SetDoneIcon(){
	var newColor : Color = Color32(254,34,34,255);
	renderer.material.color  = newColor;
	//renderer.material.color = Color.red;
	//GetComponent(SpriteRenderer).sprite = doneSprite;
}
function Update () {
	if(cam ==null){
		cam = Camera.main;
	}
	if(clickable){
		for (var i = 0; i < Input.touchCount; ++i) {
	        if (Input.GetTouch(i).phase == TouchPhase.Began) {
				var wp :Vector3 = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
		        var touchPos : Vector2 = new Vector2(wp.x, wp.y);
		        if (gameObject.GetComponent(BoxCollider2D) == Physics2D.OverlapPoint(touchPos) && Physics2D.OverlapPoint(touchPos).gameObject.transform.name == "levelSelect_"+selectedLevel)
		        {
		         	if(isOpen){	
			         	PlayerPrefs.SetInt("CurrentLevel",selectedLevel);
			 			ballScript.LoadNextLevel("Level"+selectedLevel);
		 			}
		        }  
	 	   }
 	   }  
 	   if(Input.GetKeyDown(KeyCode.Space)){
 	   		if(isOpen){       
	         	PlayerPrefs.SetInt("CurrentLevel",selectedLevel);
	 			ballScript.LoadNextLevel("Level"+selectedLevel);
 			}
 	   }
 	   
    }
}
function OnTriggerEnter2D(other: Collider2D) {
	if(!isOpen)return;
	halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
	clickable = true;
}
function OnTriggerExit2D(other: Collider2D) {
	halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	clickable = false;
}
function OnGUI(){
 	if(clickable){
		GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1));
		//GUI.Label(new Rect(nativeHorizontalResolution/2,0, 0, 0), "Click to enter level "+selectedLevel+"!", myFont);
		ShadowAndOutline.DrawOutline(new Rect(nativeHorizontalResolution/2,0, 0, 0), "Click to enter level "+selectedLevel+"!", myFont,outerColor, innerColor, 5f );
		
	}
}