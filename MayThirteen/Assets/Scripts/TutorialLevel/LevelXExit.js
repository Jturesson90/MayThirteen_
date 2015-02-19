#pragma strict
private var camSci : CameraScript;
private var fadeInScript: FadeInScript;
private var myGUI : PauseScript;
private var dialog : DialogueScript;
private var hasHitExit : boolean = false;
function Awake(){
	fadeInScript = GameObject.Find("FadeInObject").GetComponent("FadeInScript");
	camSci = GameObject.Find("Camera").GetComponent("CameraScript");
	dialog = GameObject.Find("DialogueObject").GetComponent("DialogueScript");
	myGUI = Camera.main.gameObject.GetComponent("PauseScript");
}
function Start () {

}
private var timer : float = 0.0f;
function Update () {
	if(hasHitExit){
		timer += Time.deltaTime;
	}
	if(timer >1.5f){
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel("LevelSelectionLobby");
			PlayerPrefs.SetInt ("DoneFirstLevel", 1);
		}
	}
}
function OnTriggerEnter2D(coll: Collider2D) {
	if (coll.gameObject.tag == "Ball"&& !hasHitExit){
		hasHitExit = true;
		fadeInScript.fadeOut=true;
		camSci.followPlayer=false;
		dialog.Running(false);
		dialog.FadeInText("Take care, Little Rockstar..");
		myGUI.showGUI= false;
	}
}