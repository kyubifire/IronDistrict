using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	// The amount of health the enemy starts game with
	public int startingHealth = 100;

	// current health of enemy
	public int currentHealth;

	// The sound to play when the enemy dies.
	//	public AudioClip deathClip;              

	Animator anim;                                            

	// Whether the enemy is dead.   
	bool isDead;                                                       

	void Awake () {
		// Setting up the references.
		anim = GetComponent <Animator> ();
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	void Update () {
		
	}

	public void TakeDamage (int amount, Vector3 hitPoint) {
		// If the enemy is dead
		if(isDead)
			// no need to take damage so exit the function.
			return;
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0) {
			// the enemy is dead.
			Death ();
		}
	}

	void Death () {
		// The enemy is dead.
		isDead = true;
		// Tell the animator that the enemy is dead.
		anim.SetTrigger ("Dead");

		// play deatchclip music.
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();
		}
}

