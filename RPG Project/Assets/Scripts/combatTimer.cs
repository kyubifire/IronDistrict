using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class combatTimer : MonoBehaviour {
	public float timeRemaining = 6;
	public float WaitTime = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}

	void OnGUI() {
		if (timeRemaining > 0) {
							//x, y, width, height
			GUI.Label (new Rect (450, 20, 200, 100), "Time: " + (int)timeRemaining);
		} else {
			GUI.Label (new Rect (450, 20, 100, 100), "Time's Up");

			// small delay before function call
			StartCoroutine("resetTimer", WaitTime);
		}
	}

	public void resetTimer() {
		timeRemaining = 6;
	}
}
