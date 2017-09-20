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

	Animator animator;
	SpriteRenderer characterSprite;

	public bool isSwitched;
	public bool canSwitch;
	public bool canBattle;

	public PlayerHealth playerHealth;
	public EnemyHealth enemyHealth;

	public string playerName;
	public string enemyName;

	public int currentHealth;
	public int currentEnemyHealth;

	void Start() {
		time = timer.timeRemaining;
		currentHealth = (int)playerHealth;
		currentEnemyHealth = (int)enemyHealth;
		animator = GetComponent<Animator>();
		characterSprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (currentHealth <= 0) {
			animator.SetBool ("Dead", true);
		} //else if (isIncapacitated) {  			// checks if stunned or sleep, etc
		
		//} else {								// if all good

		//}

	}

	void startBattle() {

	}

	//PRINTS OUT THE HEALTH OF YOUR PLAYERS
	void checkHealth(){
		//debug if health is changing
		for (int g = 0; g < playerGears.Count; g++){
			Debug.Log (playerGears [g].playerName + ": " + yourGears [g].self.currentHealth);
		}
		for (int g = 0; g < enemyGears.Count; g++){
			Debug.Log (enemyGears [g].self.allyName + ": " + enemyGears [g].self.currentHealth);
		}
	}
}
