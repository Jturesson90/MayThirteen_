#pragma strict

private var sw : StoneWallScript;

var thisID =0;
private var dialogObj : DialogueScript;
private var newArrayHelper : boolean = true;
private var timer : float= 0f;
private var stoneWallHelper = true;
var goalFound = false;
var myDialog :String[];
var goodArray = new Array("Good, lets continue..","");
function Awake () {
	sw  = GameObject.Find("StoneWall").GetComponent("StoneWallScript");
	dialogObj = GameObject.FindGameObjectWithTag("Dialog").GetComponent("DialogueScript");
	
}

function Update () {
	if(!newArrayHelper && thisID == 12){
		timer += Time.deltaTime;
		if(timer >2f){
			if(Camera.main.transform.position.z < -16 && stoneWallHelper){
				print("CAMERA OK");
				dialogObj.NewDialogArray(goodArray);
				
				StoneWall();
				
			}
		}
	}if(!newArrayHelper && thisID == 13 && stoneWallHelper && goalFound){
		dialogObj.NewDialogArray(goodArray);
		StoneWall();
		stoneWallHelper = false;
	}
}
function StoneWall(){
	if(GameObject.Find("StoneWall") != null){
		for(var stone : GameObject in GameObject.FindGameObjectsWithTag("StoneWall"))
		{
			var stoneScript : StoneWallScript = stone.GetComponent("StoneWallScript");
			stoneScript.StartAnimationWithDelay(thisID,0f);	
			stoneWallHelper = false;
		}
	}
}
function GoalFound(index : boolean){
	goalFound = index;
}
function OnTriggerEnter2D(coll : Collider2D){
		if (coll.gameObject.tag == "Ball" && newArrayHelper){
			
			dialogObj.NewDialogArray(myDialog);
			newArrayHelper = false;
		}
}
