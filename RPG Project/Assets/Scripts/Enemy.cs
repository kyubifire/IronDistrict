using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// attributes
	public int startingHealth = 100;				// The amount of health the enemy starts game with
	public int currentHealth;
	Animator anim;
	public string enemyName;
	public List<Gear> enemyGears;
	SpriteRenderer enemySprite;
	public string attackType;

	// enemy sounds
	//	public AudioClip deathClip;              
	                                           
	bool isDead;
	bool canSwitch;
	bool isSwitched;

	public int numGears;
	public int gearIndex;

	// Use this for initialization
	void Start () {
		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemySprite = GetComponent<SpriteRenderer>();
		currentHealth = startingHealth;				// Setting the current health when the enemy first spawns.
		isSwitched = false;
		canSwitch = true;
		numGears = enemyGears.Count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage (int amount, Vector3 hitPoint) {
		// If the enemy is dead
		if(isDead)
			// no need to take damage so exit the function.
			return;
		// Reduce the current health by the amount of damage sustained.
		//currentHealth -= amount;
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0) {
			// the enemy is dead.
			enemyDeath ();
		}
	}

	Gear chooseGear() {
		// return a random index of number of gears in list for enemy selection
		gearIndex = Random.Range (0, numGears);
		Gear selectedGear = enemyGears [gearIndex];
		return selectedGear;
	}

	string getGearType() {
		Gear nextGear = enemyGears [enemyGears.Count - 1];
		return nextGear.self.attackType;
	}

	void deleteGear() {
		//delete
		enemyGears.RemoveAt(enemyGears.Count-1);
	}

	void enemyDeath () {
		// The enemy is dead.
		isDead = true;
		// Tell the animator that the enemy is dead.
		Debug.Log(enemyName + "died");
		anim.SetTrigger ("Dead");
		// play deatchclip music.
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();
	}
}
