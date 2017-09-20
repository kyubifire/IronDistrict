using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
    public List<Gear> playerGears;
	public List<Gear> enemyGears;

    public int oldKey;
    public int newKey;

	public combatTimer timer;
	public float time;

	public Player playerSelf;
	public Enemy enemySelf;

	public PlayerHealth playerHealth;
	public EnemyHealth enemyHealth;

	//public string playerName;
	//public string enemyName;

	public int currentHealth;
	public int currentEnemyHealth;

	Animator playerAnimator;
	Animator enemyAnimator;

	SpriteRenderer playerSpr;
	SpriteRenderer enemySpr;

	private string playerAttackType;
	private string enemyAttackType;

	public bool isSwitched;
	public bool canSwitch;
	public bool canBattle;

	void Start() {
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
		} else if (currentEnemyHealth <= 0) {
			enemyAnimator.SetBool ("Dead", true);
		} else {
			startBattle ();
		}
	}

	void startBattle() {
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
}
