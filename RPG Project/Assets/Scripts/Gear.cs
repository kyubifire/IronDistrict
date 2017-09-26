using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {
	public bool isAlive;
	public string attackType;
	private Animator gearAnimator;

	// Use this for initialization
	void Start () {
		isAlive = true;
		gearAnimator = GetComponent<Animator>();
		updateGearSprite();
		//Debug.Log ("** IN GEAR CLASS **");
	}
	
	// Update is called once per frame
	void Update () {
		updateGearSprite();
	}

	void updateGearSprite(){
		//attackType = self.attackType;
		if (attackType == "green") {
			gearAnimator.SetInteger ("gearType", 0);
		} else if (attackType == "blue") {
			gearAnimator.SetInteger ("gearType", 1);
		} else if (attackType == "red") {
			gearAnimator.SetInteger ("gearType", 2);
		}
		if (!isAlive) {
			gearAnimator.SetBool ("dead", true);
		}
	}
}
