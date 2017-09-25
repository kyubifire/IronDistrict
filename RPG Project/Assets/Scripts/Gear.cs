using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {
	public bool isAlive;
	public string attackType;

	private Animator gearAnimator;

	//int playerHealth;
	//int enemyHealth;

	//private Player playerSelf;
	//private Enemy enemySelf;

	// Use this for initialization
	void Start () {
		isAlive = true;
		gearAnimator = GetComponent<Animator>();
		updateGearSprite();

		//playerSelf = gameObject.GetComponent<Player> ();
		//enemySelf = gameObject.GetComponent<Enemy> ();

		//Debug.Log ("Player self:" + playerSelf);
		Debug.Log ("** IN GEAR CLASS **");

		//playerHealth = playerSelf.currentHealth;
		//enemyHealth = enemySelf.currentHealth;

		//Debug.Log ("Player Health (FROM GEAR CLASS): " + playerHealth);
		//Debug.Log ("Enemy Health (FROM GEAR CLASS): " + enemyHealth);
	}
	
	// Update is called once per frame
	void Update () {
		updateGearSprite();

		//if (playerHealth < 0 || enemyHealth < 0) {
		//	isAlive = false;
		//}
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
