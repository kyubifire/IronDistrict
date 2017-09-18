using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManagement : MonoBehaviour {
	public List<Gear> yourGears;
	public List<Gear> enemyGears;
	public Text txt;
	bool canSwitch;
	private bool canBattle;
	private bool isSwitched;

	// Use this for initialization
	void Start () {
		isSwitched = false;
		canSwitch = true;
		canBattle = true;
		txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.Return) && canBattle == true) {
			canBattle = false;
			matchStart();
		}
		//does not work if you cant switch the  pieces
		if (canSwitch == true) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (!isSwitched) {
					//isSwitched = true;
					Gear temp = yourGears [0];
					Debug.Log ("switched");
					yourGears [0] = yourGears [1];
					yourGears [1] = temp;
					if (yourGears.Count >= 2) {
						string tempType = yourGears [0].self.attackType;
						yourGears [0].self.attackType = yourGears [1].self.attackType;
						yourGears [1].self.attackType = tempType;
					}            
				}
			}
		}
	}

	void checkGearDeath() {
		//checks if your character has no health
		for (int g = 0; g < yourGears.Count; g++){
			if(yourGears[g].self.currentHealth <= 0){
				yourGears.Remove (yourGears [g]);
			}
		}
		for (int g = 0; g < enemyGears.Count; g++){
			if(enemyGears[g].self.currentHealth <= 0){
				enemyGears.Remove (enemyGears [g]);
			}
		}
	}

	void checkHealth() {
		//debug if health is changing
		for (int g = 0; g < yourGears.Count; g++){
			Debug.Log (yourGears [g].self.allyName + ": " + yourGears [g].self.currentHealth);
		}
		for (int g = 0; g < enemyGears.Count; g++){
			Debug.Log (enemyGears [g].self.allyName + ": " + enemyGears [g].self.currentHealth);
		}
	}
		
	public void matchStart() {
		//when the battle phase of minigame begins
		canSwitch = false;
		//stops you from switching the pieces mid battle
		int currentGear = 0;
		//goes through both arrays 
		while (currentGear < yourGears.Count && currentGear < enemyGears.Count) {
			yourGears [currentGear].battle (enemyGears [currentGear]);
			checkGearDeath ();
			currentGear = currentGear + 1;
		} 
		int secondGear = 0;
		//if there are more team members than enemies
		if (yourGears.Count > enemyGears.Count) {
			while (currentGear < yourGears.Count) {
				if (secondGear >= enemyGears.Count) {
					secondGear = 0;
				}
				yourGears [currentGear].battle (enemyGears [secondGear]);
				checkGearDeath ();
				currentGear = currentGear + 1;
				secondGear = secondGear + 1;
			}
		//if there are more enmeies than team members 
		} else if (yourGears.Count< enemyGears.Count) {
			while (currentGear < enemyGears.Count) {
				if (secondGear >= yourGears.Count) {
					secondGear = 0;
				}
				yourGears [secondGear].battle (enemyGears [currentGear]);
				checkGearDeath ();
				currentGear = currentGear + 1;
				secondGear = secondGear + 1;
			}
		}

		checkGearDeath ();
		checkHealth ();
		//checks for victory or loss conditions 
		if (enemyGears.Count == 0) {
			Debug.Log ("Victory, Enemies Dead");
		} else if (yourGears.Count == 0) {
			Debug.Log ("You Lose, Your Team Is Dead");
		} else {
			//initiates battle agaain
			canBattle = true;
			canSwitch = true;
			Debug.Log ("Press Enter to fight Again");

		}
	}
}
