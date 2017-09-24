using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	// attributes
	public int startingHealth = 50;				// The amount of health the enemy starts game with
	public int currentHealth;
	public int maxDamage;
	public int currentDamage;
	Animator anim;
	public string enemyName;
	public List<Gear> enemyGears;
	//SpriteRenderer enemySprite;
	public string attackType;

	// enemy-specific UI things
	//public Slider healthSlider;

	// enemy sounds
	//	public AudioClip deathClip;              
	                                           
	bool isDead;
	bool damaged;
	bool canSwitch;
	bool isSwitched;

	private int numGears;
	private int gearIndex;

	// Use this for initialization
	void Start () {
		// Setting up the references.
		anim = GetComponent <Animator> ();
		//enemySprite = GetComponent<SpriteRenderer>();
		currentHealth = startingHealth;				// Setting the current health when the enemy first spawns.
		isSwitched = false;
		canSwitch = true;
		numGears = enemyGears.Count;
		currentDamage = maxDamage;
	}
	
	// Update is called once per frame
	void Update () {
		//if (damaged) {
		//	damaged = true;
			//takeDamage ();
			//damageImage.color = flashColor;
		} 
		//else {
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		//}
		//damaged = false;
		
	//}

	//public void TakeDamage (int amount, Vector3 hitPoint) {
		// If the enemy is dead
		//if(isDead)
			// no need to take damage so exit the function.
		//	return;
		// Reduce the current health by the amount of damage sustained.
		//currentHealth -= amount;
		// If the current health is less than or equal to zero...
		//if(currentHealth <= 0) {
			// the enemy is dead.
		//	enemyDeath ();
		//}
	//}

	public void takeDamage (int amountOfDamage) {
		damaged = true;
		currentHealth -= amountOfDamage;
		//.value = currentHealth;
		//playerAudio.Play ();
		if (currentHealth <= 0 && isDead) {
			enemyDeath ();
		}
	}

	Gear chooseGear() {
		// return a random index of number of gears in list for enemy selection
		gearIndex = Random.Range (0, numGears);
		Gear selectedGear = enemyGears [gearIndex];
		return selectedGear;
	}

	//string 
	//void getGearType() {
	//	Gear nextGear = enemyGears [enemyGears.Count - 1];
		//return nextGear.self.attackType;
	//	return;
	//}

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
