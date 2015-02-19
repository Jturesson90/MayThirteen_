#pragma strict

function Start () {

}

function Update () {
	
}

function OnTriggerEnter2D(coll : Collider2D){
	if (coll.gameObject.tag == "Ball"){
		animation.Play("PlipAway", PlayMode.StopAll);
	}
}