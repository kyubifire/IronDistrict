using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {
	public GameObject dialogueBox;
	public Text dialogueText;

	public bool dialogueActive;

	void Start () {
	
	}

	void Update() {
		if (dialogueActive && Input.GetKeyDown (KeyCode.Space)) {
			dialogueBox.SetActive (false);
			dialogueActive = false;
		}
	}

	public void showBox(string dialogue) {
		dialogueActive = true;
		dialogueBox.SetActive(true);
		dialogueText.text = dialogue;
	}
}
