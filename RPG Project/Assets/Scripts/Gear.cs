using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {
	public bool isAlive;
	private string playerAttackType;
	private string enemyAttackType;
	//public Ally self;
	public Player playerSelf;
	public Enemy enemySelf;

	Animator playerAnimator;
	Animator enemyAnimator;

	// Use this for initialization
	void Start () {
		isAlive = true;
		playerAttackType = playerSelf.attackType;
		enemyAttackType = enemySelf.attackType;
		playerAnimator = playerSelf.GetComponent <Animator>();
		enemyAnimator = enemySelf.GetComponent<Animator> ();
		updateGearSprite();
	}

	void updateGearSprite(){
		//attackType = self.attackType;
		if (playerSelf.attackType == "green") {
			playerAnimator.SetInteger ("gearType", 0);
		} else if (playerSelf.attackType == "blue") {
			playerAnimator.SetInteger ("gearType", 1);
		} else if (playerSelf.attackType == "red") {
			playerAnimator.SetInteger ("gearType", 2);
		}
		if (!isAlive) {
			playerAnimator.SetBool ("dead", true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateGearSprite();
		if (playerSelf.currentHealth < 0 || enemySelf.currentHealth) {
			isAlive = false;
		}
	}

	public void battle(Gear other){
		//if self.attackType == red
		if (attackType == "red" && other.attackType == "blue") {
			//if bad matchup
			badAttack (other.self);
		} else if (attackType == "red" && other.attackType == "red") {
			//if eh matchup
			goodAttack (other.self);
		} else if (attackType == "red" && other.attackType == "green") {
			//if super matchup
			superAttack (other.self);
		}

		//if self.attackType == blue
		else if (attackType == "blue" && other.attackType == "green") {
			//if bad matchup
			badAttack (other.self);
		} else if (attackType == "blue" && other.attackType == "blue") {
			//if eh matchup
			goodAttack (other.self);
		} else if (attackType == "blue" && other.attackType == "red") {
			//if super matchup
			superAttack (other.self);
		}
		//if self.attackType == green
		else if (attackType == "green" && other.attackType == "red") {
			//if bad matchup
			badAttack (other.self);
		} else if (attackType == "green" && other.attackType == "green") {
			//if eh matchup
			goodAttack (other.self);
		} else if (attackType == "green" && other.attackType == "blue") {
			//if super matchup
			superAttack (other.self);
		} else {
			Debug.Log ("You're A dipSHIt check your type names");
		}
	}
		
	void badAttack(Ally other){
	//bad attack hurts self
		self.damage(other.currentDamage);
		Debug.Log (other.name + " Bad Attacks " + self.name);
	}

	void goodAttack(Ally other){
	//good Attack hurts enemy and ally
		other.damage(self.currentDamage);
		self.damage(other.currentDamage);
		Debug.Log (self.name + " Good Attacks " + other.name);
	}

	void superAttack(Ally other){
	//super attack hurts enemy
		other.damage (self.currentDamage);
		Debug.Log (self.name + " Super Attacks " + other.name);
	}
}
