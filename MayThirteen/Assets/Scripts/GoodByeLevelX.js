#pragma strict

var myDialog :String[];
private var dialogObj : DialogueScript;
private var  newArrayHelper = true;
function Awake(){
	dialogObj = GameObject.FindGameObjectWithTag("Dialog").GetComponent("DialogueScript");
}
function Start () {

}

function Update () {

}
function OnTriggerEnter2D(coll : Collider2D){
		if (coll.gameObject.tag == "Ball" && newArrayHelper){
			
			dialogObj.NewDialogArray(myDialog);
			newArrayHelper = false;
		}
}