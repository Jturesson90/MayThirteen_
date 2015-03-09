#pragma strict

function Start () {
	GetComponent.<Renderer>().material.color = Color.clear;
}

function Update () {
	GetComponent.<Renderer>().material.color = Color.Lerp(GetComponent.<Renderer>().material.color, Color.white,Time.deltaTime);
}
