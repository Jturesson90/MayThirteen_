#pragma strict

private var halo : Component;
var selectedLevel: int;
private var clickable :boolean;
private var cam : Camera;

private var ballScript : BallScript;
private var isOpen : boolean;

private var state :LevelState;
private var uiHelper : UIHelper;

function Awake(){
	
	halo = GetComponent("Halo"); 
	halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent("BallScript");
	uiHelper = GameObject.Find("UIHelper").GetComponent("UIHelper");
	
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
	uiHelper.ShowLevelText(selectedLevel);
}
function OnTriggerExit2D(other: Collider2D) {
	halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	clickable = false;
	uiHelper.HideLevelText();
}
