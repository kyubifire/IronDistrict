using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
	//public List<Player> allies;
	public List<Enemy> enemy;

    public List<Gear> playerGears;
	public List<Gear> enemyGears;

	combatTimer timer;
	private float time;

	Player playerSelf;
	Enemy enemySelf;


	private int currentHealth;
	private int currentEnemyHealth;

	Animator playerAnimator;
	Animator enemyAnimator;

	SpriteRenderer playerSpr;
	SpriteRenderer enemySpr;

	private string playerAttackType;
	private string enemyAttackType;

	bool isSwitched;
	bool canSwitch;
	bool canBattle;
	bool isTimeExpired;

	private int oldKey;
	private int newKey;

	void Start() {

		oldKey = 0;
		newKey = 0;
		isTimeExpired = false;
		time = timer.timeRemaining;

		currentHealth = playerSelf.startingHealth;
		currentEnemyHealth = enemySelf.startingHealth;

		playerAnimator = playerSelf.GetComponent<Animator>();
		enemyAnimator = enemySelf.GetComponent<Animator> ();

		playerSpr = playerSelf.GetComponent<SpriteRenderer>();
		enemySpr = enemySelf.GetComponent<SpriteRenderer> ();

		playerAttackType = playerSelf.attackType;
		enemyAttackType = enemySelf.attackType;
	}

	void Update() {
		if (currentHealth <= 0) {
			playerAnimator.SetBool ("Dead", true);
		}

		if (currentEnemyHealth <= 0) {
			enemyAnimator.SetBool ("Dead", true);
		} 


		//else {
		//	startBattle ();
		//}
	}

	public void switchGears() {
		//Switch Gears. 	Should add some kind of visual gear selection
		if (!isTimeExpired) {
			if (oldKey == 0) {
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					oldKey = 1;
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					oldKey = 2;
				} else if (Input.GetKeyDown (KeyCode.Alpha3) && playerGears.Count >= 3) {
					oldKey = 3;
				} else if (Input.GetKeyDown (KeyCode.Alpha4) && playerGears.Count >= 4) {
					oldKey = 4;
				}
			} else {
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					newKey = 1;
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					newKey = 2;
				} else if (Input.GetKeyDown (KeyCode.Alpha3) && playerGears.Count >= 3) {
					newKey = 3;
				} else if (Input.GetKeyDown (KeyCode.Alpha4) && playerGears.Count >= 4) {
					newKey = 4;
				}
				//Switch gear positions
				Gear temp = playerGears [oldKey - 1];
				playerGears [oldKey - 1] = playerGears [newKey - 1];
				playerGears [newKey - 1] = temp;
				oldKey = 0;
				newKey = 0;
			}
		}
	}

	void startBattle() {
		if (time > 0) { 
			if (playerAttackType == "red" && enemyAttackType == "blue") {
				//if bad matchup
				badAttack ();
			} else if (playerAttackType == "red" && enemyAttackType == "red") {
				//if eh matchup
				goodAttack ();
			} else if (playerAttackType == "red" && enemyAttackType == "green") {
				//if super matchup
				superAttack ();
			} else if (playerAttackType == "blue" && enemyAttackType == "green") {
				//if bad matchup
				badAttack ();
			} else if (playerAttackType == "blue" && enemyAttackType == "blue") {
				//if eh matchup
				goodAttack ();
			} else if (playerAttackType == "blue" && enemyAttackType == "red") {
				//if super matchup
				superAttack ();
			} else if (playerAttackType == "green" && enemyAttackType == "red") {
				//if bad matchup
				badAttack ();
			} else if (playerAttackType == "green" && enemyAttackType == "green") {
				//if eh matchup
				goodAttack ();
			} else if (playerAttackType == "green" && enemyAttackType == "blue") {
				//if super matchup
				superAttack ();
			} else {
				Debug.Log ("You're A dipSHIt check your type names");
			}
		}
	}

	void badAttack() {
		//bad attack hurts self
		playerSelf.takeDamage(enemySelf.currentDamage);
		Debug.Log (enemySelf.enemyName + " Bad Attacks " + playerSelf.playerName);
	}

	void goodAttack(){
		//good Attack hurts enemy and ally
		enemySelf.takeDamage(playerSelf.currentDamage);
		playerSelf.takeDamage(enemySelf.currentDamage);
		Debug.Log (playerSelf.playerName + " Good Attacks " + enemySelf.enemyName);
	}

	void superAttack(){
		//super attack hurts enemy
		enemySelf.takeDamage (playerSelf.currentDamage);
		Debug.Log (playerSelf.playerName + " Super Attacks " + enemySelf.enemyName);
	}

	//PRINTS OUT THE HEALTH OF YOUR PLAYERS
	void checkHealth(){
		//debug if health is changing
		for (int g = 0; g < playerGears.Count; g++) {
			Debug.Log (playerGears [g].playerSelf.playerName + ": " + playerGears [g].playerSelf.currentHealth);
		}

		for (int g = 0; g < enemyGears.Count; g++){
			Debug.Log (enemyGears [g].playerSelf.playerName + ": " + enemyGears [g].playerSelf.currentHealth);
		}
	}

	void timeExpired() {
		if (time < 0) {
			isTimeExpired = true;
		}
	}
}
