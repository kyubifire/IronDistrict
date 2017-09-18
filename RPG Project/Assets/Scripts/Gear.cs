using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour {
	public bool isAlive;			// check if character is dead.  If so, disable gears for that character
	private string attackType;
	public Ally self;
	Animator animator;

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);		// red

	AudioSource playerAudio;
	bool isDead;				// check for if player is dead or not
	bool damaged;

	// Use this for initialization
	void Start () {
		isAlive = true;
		attackType = self.attackType;
		animator = GetComponent <Animator>();
		// set initial health of player
		currentHealth = startingHealth;
		updateGearSprite();
	}

	void updateGearSprite() {
		attackType = self.attackType;
		if (attackType == "green") {
			animator.SetInteger ("gearType", 0);
		} else if (attackType == "blue") {
			animator.SetInteger ("gearType", 1);
		} else if (attackType == "red") {
			animator.SetInteger ("gearType", 2);
		}

		if (!isAlive) {
			animator.SetBool ("dead", true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// When the player or an ally is hit the screen will  flash red to signify damage
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		updateGearSprite();
		if (self.currentHealth < 0) {
			isAlive = false;
		}
	}
		
	public void battle(Gear other) {
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

	void goodAttack(Ally other) {
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

	public void takeDamage (int amountOfDamage) {
		damaged = true;
		currentHealth -= amountOfDamage;
		healthSlider.value = currentHealth;
		playerAudio.Play ();

		if (currentHealth <= 0 && isDead) {
			playerDeath ();
		}
	}

	void playerDeath() {
		isDead = true;
		//		anim.SetTrigger ("Die");
		//playerController.enabled = false;
		//playerAudio.clip = deatchClip;
		//playerAudio.Play ();
	}
}
