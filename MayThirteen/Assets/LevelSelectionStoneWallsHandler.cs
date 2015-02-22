using UnityEngine;
using System.Collections;

public class LevelSelectionStoneWallsHandler : MonoBehaviour
{

		// Use this for initialization
		private int levelsDone;
		private GameObject[] stoneWalls;

		private float stoneWallDelay = 1f;


		void Awake ()
		{
				levelsDone = PlayerPrefs.GetInt ("LevelsDone", 0);
				stoneWalls = GameObject.FindGameObjectsWithTag ("StoneWall");
				

		}
		private void DisableStoneWalls ()
		{
				for (int i = 0; i < stoneWalls.Length; i++) {
						StoneWall script = stoneWalls [i].GetComponent<StoneWall> ();
						if (script.wallIndex < levelsDone) {
								DisableStoneWall (stoneWalls [i]);
						} else if (script.wallIndex == levelsDone) {
								StartCoroutine (StartStoneWallAnimation (script));
						}
				}
			
		}
		void Start ()
		{
				DisableStoneWalls ();
		}
		
		
		private void DisableStoneWall (GameObject stoneWall)
		{
				stoneWall.SetActive (false);
				
		}
		IEnumerator StartStoneWallAnimation (StoneWall sw)
		{
				yield return new WaitForSeconds (stoneWallDelay);
				sw.StartStoneWallAnimation ();
				
		}
}
