#pragma strict
private var helper = false;
function Start () {

}

function Update () {

}

function OnTriggerEnter2D(coll : Collider2D){
		if (coll.gameObject.tag == "Ball" && !helper){
			for(var stone : GameObject in GameObject.FindGameObjectsWithTag("StoneWallTrigger"))
					{
						var stoneScript : LevelXOpenDoors = stone.GetComponent("LevelXOpenDoors");
						stoneScript.GoalFound(true);
						helper = true;
					}
		}
		
}
