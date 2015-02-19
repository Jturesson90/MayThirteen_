#pragma strict

function Start () {
	renderer.material.color = Color.clear;
}

function Update () {
	renderer.material.color = Color.Lerp(renderer.material.color, Color.white,Time.deltaTime);
}
