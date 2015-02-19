#pragma strict
var spinSpeed : float = 1;
function Start () {

}

function Update () {
	transform.Rotate(0, 0, Time.deltaTime* spinSpeed, Space.World);
}

function OnCollisionEnter2D(coll: Collision2D) {
	if (coll.gameObject.tag == "Ball"){
		coll.gameObject.SendMessage("Die",transform.position);
	}
}