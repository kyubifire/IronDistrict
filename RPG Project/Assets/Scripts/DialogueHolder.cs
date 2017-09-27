using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueHolder : MonoBehaviour {
	public string dialogue;
	private NPCDialogueManager npcDialogueManager;

	// Use this for initialization
	void Start () {
		npcDialogueManager = FindObjectOfType<NPCDialogueManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			if (Input.GetKeyUp(KeyCode.Space)) {
				npcDialogueManager.showBox (dialogue);
			}
		}
	}
}
