using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatTimer : MonoBehaviour {
	float timeRemaining = 10;

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
			GUI.Label (new Rect (100, 100, 100, 100), "Time's Up");
		}
	}
}
