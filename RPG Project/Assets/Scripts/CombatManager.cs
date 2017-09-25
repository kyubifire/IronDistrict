using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
	public List<Player> allies;
	public List <Enemy> enemies;

	public List<Gear> playerGears;	
	public List<Gear> enemyGears;

	Player playerSelf;
	Enemy enemySelf;

	private int currentHealth;
	private int currentEnemyHealth;

	private Animator playerAnimator;
	private Animator enemyAnimator;

	private string playerAttackType;
	private string enemyAttackType;

	private bool isSwitched;
	private bool canSwitch;
	private bool canBattle;
	private bool isTimeExpired;
	private bool attackDone;

	//private int oldKey;
	//private int newKey;

	private float time;
	private int numEnemyGears;
	private int gearIndex;

	void Start() {
		//oldKey = 0;
		//newKey = 0;
		canSwitch = true;
		canBattle = true;
		attackDone = false;
		isSwitched = false;
		isTimeExpired = false;
		numEnemyGears = enemyGears.Count;
		time = 5;
		playerSelf = FindObjectOfType<Player> ();
		enemySelf = FindObjectOfType<Enemy> ();

		//Debug.Log ("** IN COMBATMANAGER**");
		Debug.Log ("Player self:" + playerSelf.playerName);
		Debug.Log ("Enemy self:" + enemySelf.enemyName);

		currentHealth = playerSelf.startingHealth;
		currentEnemyHealth = enemySelf.startingHealth;
	
		Debug.Log (playerSelf.playerName + "Current Player Health (COMBATMANAGER)" + currentHealth);
		Debug.Log (enemySelf.enemyName + "Current Enemy Health (COMBATMANAGER)" + currentEnemyHealth);

		playerAnimator = playerSelf.GetComponent<Animator>();
		enemyAnimator = enemySelf.GetComponent<Animator> ();
		  
		//playerSpr = playerSelf.GetComponent<SpriteRenderer>();
		//enemySpr = enemySelf.GetComponent<SpriteRenderer> ();
	}

	void Update() {
		if (currentHealth <= 0) {
			playerAnimator.SetBool ("Dead", true);
		}

		if (currentEnemyHealth <= 0) {
			enemyAnimator.SetBool ("Dead", true);
		} 
		time -= Time.deltaTime;
		Debug.Log ("Time:" + time);
		switchGears();
	}	

	public void switchGears() {
		Debug.Log ("** IN SWITCH GEARS **");
		//Switch Gears. 	Should add some kind of visual gear selection
		// accessing a list, gets gear sprite of new gear and replaces old gear that
		// set gear velocity  to a value and once timer expires use that velocity to move gear to bottom corner. Can also modify position
		if (time > 0 && canSwitch == true) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (!isSwitched) {
					Gear temp = playerGears [0];
					Debug.Log (" !!! SWITCHED !!!"); 
					if (playerGears.Count >= 2) {
						playerGears [0] = playerGears [1];
						playerGears [1] = temp;

						string tempType = playerGears [0].attackType;
						playerGears [0].attackType = playerGears [1].attackType;
						playerGears [1].attackType = tempType;
					}
					playerAttackType = playerGears [1].attackType;
				}
			}
			Gear temp2 = enemyGears[0];
			if (enemyGears.Count >= 2) {
				gearIndex = Random.Range (0, numEnemyGears);  
				enemyGears [0] = enemyGears [gearIndex];
				enemyGears [gearIndex] = temp2;

				string temp2Type = enemyGears [0].attackType;
				enemyGears [0].attackType = enemyGears [gearIndex].attackType;
				enemyGears [gearIndex].attackType = temp2Type;
			}

			enemyAttackType = enemyGears [gearIndex].attackType;
		}
		else {
			// disable switching capabilities
			canSwitch = false;
			//move gear to bottom of screen
			Vector3 newPlayerGearPos = new Vector3(2, -3,0);
			Vector3 newEnemyGearPos = new Vector3 (0, -2, 0);
			playerGears[0].transform.position = newPlayerGearPos;
			enemyGears[gearIndex].transform.position = newEnemyGearPos;
			//playerGears[0].transform.Translate(0, -(1 * Time.deltaTime), 0);
			// Initiate Battle
			if (canBattle == true && attackDone == false) {
				startBattle ();
			} else {
				timeExpired ();
			}
		}
	}

	void startBattle() {
		Debug.Log ("BATTLING!! BATTLING!!");
		Debug.Log ("Player Attack Type: " + playerAttackType);
		Debug.Log ("Enemy Attack Type:" + enemyAttackType);

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
		Debug.Log ("BadAttack");
		//bad attack hurts self
		enemyAnimator.SetBool("Attacking", true);
		playerSelf.takeDamage(enemySelf.currentDamage);
		attackDone = true;
		Debug.Log (enemySelf.enemyName + " Bad Attacks " + playerSelf.playerName);
	}

	void goodAttack(){
		Debug.Log ("GoodAttack");
		//good Attack hurts enemy and ally
		playerAnimator.SetBool("Attacking", true);
		enemyAnimator.SetBool ("Attacking", true);
		enemySelf.takeDamage(playerSelf.currentDamage);
		playerSelf.takeDamage(enemySelf.currentDamage);
		attackDone = true;
		Debug.Log (playerSelf.playerName + " Good Attacks " + enemySelf.enemyName);
	}

	void superAttack(){
		Debug.Log ("SuperAttack");
		//super attack hurts enemy
		playerAnimator.SetBool("Attacking", true);
		enemySelf.takeDamage (playerSelf.currentDamage);
		attackDone = true; 
		Debug.Log (playerSelf.playerName + " Super Attacks " + enemySelf.enemyName);
	}

	//PRINTS OUT THE HEALTH OF YOUR PLAYERS
	void checkHealth(){
		//debug if health is changing
		for (int g = 0; g < playerGears.Count; g++) {
			Debug.Log (playerSelf.playerName + ": " +playerSelf.currentHealth);
		}

		for (int g = 0; g < enemyGears.Count; g++) {
			Debug.Log (playerSelf.playerName + ": " + playerSelf.currentHealth);
		}
	}

	void timeExpired() {
		if (time <= 0) {
			isTimeExpired = true;
			resetTimer ();
		}
	}

	void resetTimer() {
		time = 5;
	}
}
