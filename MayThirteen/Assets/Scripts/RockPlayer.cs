using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class RockPlayer : MonoBehaviour
{
		
		public GameObject brokenStone;
		//private SpriteRenderer rend;
		void Start ()
		{
				if (Application.loadedLevelName == "LevelSelectionLobby") {
						SetToSavedPosition ();
				}
				
		}
		public void SetToSavedPosition ()
		{
				if (PlayerPrefsManager.HasStoneKey ()) {
						float xPos = PlayerPrefsManager.GetStonePositionX ();
						float yPos = PlayerPrefsManager.GetStonePositionY ();
						float zPos = transform.position.z;
						Vector3 newStartPosition = new Vector3 (xPos, yPos, zPos);
						transform.position = newStartPosition;
				}	
		}
		public void SavePosition ()
		{
				PlayerPrefsManager.SetStonePosition (transform.position.x, transform.position.y);
			
		}
		public void Win ()
		{
				print ("YOU WON");
		}
		
		public void Die ()
		{
				GetComponent<Renderer> ().enabled = false;
				GetComponent<Rigidbody2D> ().isKinematic = true;
				
				Component halo = GetComponent ("Halo");
				if (halo != null) {
						halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
				}
				InstantiateBrokenStone ();
				GetComponent<Collider2D> ().enabled = false;
		}
		private void InstantiateBrokenStone ()
		{
				GameObject.Instantiate (brokenStone, transform.position, Quaternion.identity);
				
		}

				
}
