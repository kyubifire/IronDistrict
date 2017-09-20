using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
	public List<Gear> playerGears;
	//public string playerName;
	public int oldKey;
	public int newKey;
	public int timer;

	// Use this for initialization
	void Start () {
		oldKey = 0;
		newKey = 0;
		//time = 11;
	}

	//public enum States {
	//	idle,
	//	attacking
	//}

	// Update is called once per frame
	void Update () {
		//Switch Gears. 	Should add some kind of visual gear selection
		if (oldKey == 0) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				oldKey = 1;
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				oldKey = 2;
			} else if (Input.GetKeyDown(KeyCode.Alpha3) && playerGears.Count >= 3) {
				oldKey = 3;
			} else if (Input.GetKeyDown(KeyCode.Alpha4) && playerGears.Count >= 4) {
				oldKey = 4;
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				newKey = 1;
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				newKey = 2;
			} else if (Input.GetKeyDown(KeyCode.Alpha3) && playerGears.Count >= 3) {
				newKey = 3;
			} else if (Input.GetKeyDown(KeyCode.Alpha4) && playerGears.Count >= 4) {
				newKey = 4;
			}

			//Switch gear positions
			Gear temp = playerGears[oldKey - 1];
			playerGears[oldKey - 1] = playerGears[newKey - 1];
			playerGears[newKey - 1] = temp;
			oldKey = 0;
			newKey = 0;
		}
	}

	//Gear #4 will be at the bottom, so that is the one that goes first.
	//this is for combat resolution purposes. Don't include animation code here.
	//string getGearType() {
	//	Gear nextGear = playerGears[playerGears.Count - 1];
	//	return nextGear.self.attackType;
	//}

	void deleteGear() {
		//delete gear sprite somehow

		//delete
		playerGears.RemoveAt(playerGears.Count - 1);
	}
}

