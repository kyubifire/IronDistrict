using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	public List<Gear> enemyGears;

	bool canSwitch;
	bool isSwitched;

	public int numGears;
	public int gearIndex;

	void Start() {
		isSwitched = false;
		canSwitch = true;
		numGears = enemyGears.Count;
	}

	void Update() {

	}

	Gear chooseGear() {
		// return a random index of number of gears in list for enemy selection
		gearIndex = Random.Range (0, numGears);
		Gear selectedGear = enemyGears [gearIndex];
		return selectedGear;
	}

	//string 
	void getGearType() {
		Gear nextGear = enemyGears [enemyGears.Count - 1];
		//return nextGear.self.attackType;
	}

	void deleteGear() {
		//delete
		enemyGears.RemoveAt(enemyGears.Count-1);
	}
}