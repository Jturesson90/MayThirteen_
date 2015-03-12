using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class LevelSelectionLevel : MonoBehaviour
{

		private bool isOpen = false;
		private bool clickable = false;

		LevelHandlerC.LevelState levelState;
		LevelSelectionManager manager;

		private UIHelper uiHelper;

		public int selectedLevel = 0;
		
		void Awake ()
		{
	
				manager = GameObject.Find ("LevelSelectionManager").GetComponent<LevelSelectionManager> ();
				SetHalo (false);	
				uiHelper = GameObject.Find ("UIHelper").GetComponent<UIHelper> ();
		}
		void Start ()
		{
				SetStar (false);
				CheckOpen ();
		}

		void Update ()
		{
				if (!clickable)
						return;
				if (Time.timeScale != 1f) {
						return;
				}
				
#if UNITY_STANDALON || UNITY_EDITOR
				HandleStandAloneInput ();		
#endif
#if UNITY_IPHONE || UNITY_ANDROID 
				HandleMobileInput ();
#endif
		}
		void HandleMobileInput ()
		{
				for (var i = 0; i < Input.touchCount; ++i) {
						if (Input.GetTouch (i).phase == TouchPhase.Began) {
								Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position);
								Vector2 touchPos = new Vector2 (wp.x, wp.y);
								if (GetComponent<Collider2D>() == Physics2D.OverlapPoint (touchPos) && Physics2D.OverlapPoint (touchPos).gameObject.transform.name == "levelSelect_" + selectedLevel) {
										if (isOpen) {	
												PlayerPrefs.SetInt ("CurrentLevel", selectedLevel);
												manager.LoadLevel ("Level" + selectedLevel);
										}
								}  
						}
				} 
		}
		void HandleStandAloneInput ()
		{
				if (Input.GetKeyDown (KeyCode.Space)) {
						if (isOpen) {       
								PlayerPrefs.SetInt ("CurrentLevel", selectedLevel);
								manager.LoadLevel ("Level" + selectedLevel);
						}
				}
		}
		public void CheckOpen ()
		{
				levelState = LevelHandlerC.handler.GetLevelState (selectedLevel);
				switch (levelState) {
				case LevelHandlerC.LevelState.OPEN:
						isOpen = true;	
						break;
				case LevelHandlerC.LevelState.NOT_OPEN:
						isOpen = false;
						SetNotOpen ();
						break;
				case LevelHandlerC.LevelState.DONE:
						isOpen = true;
						SetDoneIcon ();
						break;
				case LevelHandlerC.LevelState.DONE_STAR:
						SetDoneIcon ();
						isOpen = true;	
						SetStar (true);
						break;
				default:
			
						break;
				}
		}


		void OnTriggerEnter2D (Collider2D coll)
		{
				if (!isOpen)
						return;
				if (coll.tag != "Ball")
						return;
				SetHalo (true);
				clickable = true;
				uiHelper.ShowLevelText (selectedLevel);
				
				
		}
		void OnTriggerExit2D (Collider2D coll)
		{
				
				if (coll.tag != "Ball")
						return;
				SetHalo (false);
				clickable = false;
				uiHelper.HideLevelText ();
				
		}

		private void SetHalo (bool arg)
		{
				Component halo = GetComponent ("Halo");
				if (halo != null) {
						halo.GetType ().GetProperty ("enabled").SetValue (halo, arg, null);
				}
		}
		void SetNotOpen ()
		{
				Color newColor = new Color (0.3f, 0.3f, 0.3f);	
				GetComponent<Renderer>().material.color = newColor;
		}
		void  SetStar (bool arg)
		{
				transform.FindChild ("Star").gameObject.SetActive (arg);
		}
		void SetDoneIcon ()
		{
				Color newColor = new Color32 (254, 34, 34, 255);
				GetComponent<Renderer>().material.color = newColor;
		}
}
