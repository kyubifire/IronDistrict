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

	private int currentPlayerHealth;
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

	private Vector3 origPlGearPos;
	private Vector3 origEnGearPos;

	private float time;
	private int numEnemyGears;
	private int gearIndex;

	void Start() {
		//oldKey = 0;
		//newKey = 0;		
		time = 6;
		canSwitch = true;
		canBattle = true;
		attackDone = false;
		isSwitched = false;
		isTimeExpired = false;
		numEnemyGears = enemyGears.Count;

		playerSelf = FindObjectOfType<Player> ();
		enemySelf = FindObjectOfType<Enemy> ();

		currentPlayerHealth = playerSelf.startingHealth;
		currentEnemyHealth = enemySelf.startingHealth;
	
		playerAnimator = playerSelf.GetComponent<Animator>();
		enemyAnimator = enemySelf.GetComponent<Animator> ();

		// create variables to hold original position of gears
		origPlGearPos = playerSelf.transform.position;
		origEnGearPos = enemySelf.transform.position;

		Debug.Log ("Original position of Player Gear" + origPlGearPos);
		Debug.Log ("Original position of Enemy Gear" + origEnGearPos);
	}

	void Update() {
		//for (int g = 0; g < playerGears.Count; g++) {
		//	Debug.Log (playerSelf.playerName + "Current Player Health (COMBATMANAGER): " + currentPlayerHealth);
		//}

		//for (int g = 0; g < enemyGears.Count; g++) {
		//	Debug.Log (enemySelf.enemyName + "Current Enemy Health (COMBATMANAGER): " + currentEnemyHealth);
		//}

		//Debug.Log ("** IN COMBATMANAGER**");
		//Debug.Log ("Player self:" + playerSelf.playerName);
		//Debug.Log ("Enemy self:" + enemySelf.enemyName);

		if (currentPlayerHealth <= 0) {
			playerAnimator.SetBool ("Dead", true);
		}

		if (currentEnemyHealth <= 0) {
			enemyAnimator.SetBool ("Dead", true);
		}
			
		time -= Time.deltaTime;
		Debug.Log ("Time:" + time);
		Debug.Log("?? IS TIME EXPIRED?? " + isTimeExpired);
		if (!isTimeExpired) {
			switchGears ();
			//timeExpired ();
		}
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
		} else {
			// disable switching capabilities
			canSwitch = false;
			// change flag for timer to true
			isTimeExpired = true;
			//move gear to bottom of screen

			//Vector3 newPlayerGearPos = new  Vector3(-2, origPlGearPos.y, origPlGearPos.z);
			//Vector2 newEnemyGearPos = new Vector3 (0, -1);
			//playerGears[0].transform.position = newPlayerGearPos;
			//enemyGears[gearIndex].transform.position = newEnemyGearPos;
			//playerGears[0].transform.Translate(0, -(1 * Time.deltaTime), 0);
			//enemyGears [gearIndex].transform.Translate ((-1 * Time.deltaTime), 0, 0);
			//playerGears[0].transform.position = Vector3.Lerp(origPlGearPos, newPlayerGearPos, (Time.time - time) / time);

			// Initiate Battle
			if (canBattle == true && attackDone == false && isTimeExpired) {
				startBattle ();
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

		timeExpired ();
	}
	void badAttack() {
		Debug.Log ("BadAttack");
		//bad attack hurts self
		enemyAnimator.SetBool("Attacking", true);
		playerSelf.takeDamage(enemySelf.currentDamage);
		attackDone = true;
		Debug.Log (enemySelf.enemyName + " Bad Attacks " + playerSelf.playerName);
		timeExpired ();
	}

	void goodAttack() {
		Debug.Log ("GoodAttack");
		//good Attack hurts enemy and ally
		playerAnimator.SetBool("Attacking", true);
		enemyAnimator.SetBool ("Attacking", true);
		enemySelf.takeDamage(playerSelf.currentDamage);
		playerSelf.takeDamage(enemySelf.currentDamage);
		attackDone = true;
		Debug.Log (playerSelf.playerName + " Good Attacks " + enemySelf.enemyName);
		timeExpired ();
	}

	void superAttack() {
		Debug.Log ("SuperAttack");
		//super attack hurts enemy
		playerAnimator.SetBool("Attacking", true);
		enemySelf.takeDamage (playerSelf.currentDamage);
		attackDone = true; 
		Debug.Log (playerSelf.playerName + " Super Attacks " + enemySelf.enemyName);
		timeExpired ();
	}

	//PRINTS OUT THE HEALTH OF YOUR PLAYERS
	void checkHealth(){
		Debug.Log ("** CHECKING CHARACTERS HEALTH ** ");
		//debug if health is changing
		for (int g = 0; g < playerGears.Count; g++) {
			Debug.Log (playerSelf.playerName + ": " +playerSelf.currentHealth);
		}

		for (int g = 0; g < enemyGears.Count; g++) {
			Debug.Log (playerSelf.playerName + ": " + playerSelf.currentHealth);
		}
	}

	void timeExpired() {
		Debug.Log ("** SEEING IF TIME EXPIRED **");
		if (time <= 0) {
			isTimeExpired = true;
			resetTimer ();
		}
	}

	void resetTimer() {
		Debug.Log ("** RESETTING TIMER **");
		// reset bool so battle is no longer initiated.
		attackDone = false;
		// re-enable gear switching
		canSwitch = true;
		time = 6;
		// time is no longer expired, reset bool
		isTimeExpired = false;
		// reset position of gears
		resetGearPos();
	}

	void resetGearPos() {
		Debug.Log (" ** RESETTING GEAR POSITIONS **");

		// change gear position back to original starting positions for enemy and player
		playerGears[0].transform.position = origPlGearPos;
		enemyGears[gearIndex].transform.position = origEnGearPos;

		Debug.Log (playerGears[0].transform.position);
		Debug.Log (enemyGears[gearIndex].transform.position);

		// set animators back to idle state
		playerAnimator.SetBool("Attacking", false);
		enemyAnimator.SetBool ("Attacking", false);
		// go back to switchGears
		switchGears();
	}
}
