using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour {
	DialogueParser parser;

	public string dialogue;
	public string name;
	public Sprite pose;
	public string position;
	int lineNum;

	public GUIStyle customStyle, customStyleName;

	// Use this for initialization
	void Start () {
		dialogue = "";
		lineNum = 0;
		parser = GameObject.Find ("DialogueParser").GetComponent<DialogueParser>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)) {
			ResetImages ();
			name = parser.GetName (lineNum);
			dialogue = parser.GetContent (lineNum);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition (lineNum);
			DisplayImages ();
			lineNum++;
		}
	}	

	void ResetImages() {
		Debug.Log ("In Reset images");
		if (name != "") {
			GameObject character = GameObject.Find (name);
			Debug.Log ("Character speaking now: " + character);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer> ();
			Debug.Log ("Current sprite: " + currSprite);
			currSprite.sprite = null;
			//Debug.Log ("setting sprite to null");
		}
	}

	void DisplayImages() {
		Debug.Log ("In display Images");
		if (name != "") {
			GameObject character = GameObject.Find (name);
			Debug.Log ("current character is : " + character);
			SetSpritePositions (character);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer> ();
			Debug.Log ("CURRENT SPRITE: " + currSprite);
			currSprite.sprite = pose;
		}
	}

	void SetSpritePositions (GameObject spriteObj) {
		Debug.Log ("In SETSPRITEPOSITIONS");
		if (position == "L") {
			spriteObj.transform.position = new Vector3 (-6,2,0); // set according to width, don't hard code values
		}

		if (position == "R") {
			spriteObj.transform.position = new Vector3 (6,2,0);
		}
	}

	void OnGUI() {
		// New rect (how far left gui stretch, how high up, how far right, how far down)
		dialogue = GUI.TextField (new Rect(50, 375, 1300, 210), dialogue, customStyle);
		name = GUI.TextField (new Rect(600, 350, 200, 50), name, customStyleName);
	}
}
