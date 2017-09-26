using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public Gear playerGear;
	// player attributes
	public int startingHealth = 100;
	public int currentHealth;
	public string playerName;
	public int maxDamage;
	public int currentDamage;
	public Animator anim;
	PlayerController playerController;
	private string playerAttackType;						// this only a string we can input gear type into.. Needs to be inherited based on gear itself (inheriteed from gear class)

	// player-specific UI things
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	// player sounds
	//public AudioClip deatchClip;
	//AudioSource playerAudio;

	public bool isDead;
	public bool damaged;

	// Use this for initialization
	void Start () {
		//GameObject gearGameObject = GameObject.Find("Gear");
		anim = GetComponent<Animator> ();
		playerController = GetComponent<PlayerController> ();
		// set initial health of player
		currentHealth = startingHealth;
		currentDamage = maxDamage;
		playerGear = GetComponent<Gear> ();
		//playerAttackType = playerGear.attackType;

		//Debug.Log (playerName + " Health :" + currentHealth);
		//Debug.Log (playerName + " Damage :" + currentDamage);
	}
		
	// Update is called once per frame
	void Update () {
	}

//	public void switchGears() {
//		//Switch Gears. 	Should add some kind of visual gear selection
//		if (!isTimeExpired) {
//			if (oldKey == 0) {
//				if (Input.GetKeyDown (KeyCode.Alpha1)) {
//					oldKey = 1;
//				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
//					oldKey = 2;
//				} else if (Input.GetKeyDown (KeyCode.Alpha3) && playerGears.Count >= 3) {
//					oldKey = 3;
//				} else if (Input.GetKeyDown (KeyCode.Alpha4) && playerGears.Count >= 4) {
//					oldKey = 4;
//				}
//			} else {
//				if (Input.GetKeyDown (KeyCode.Alpha1)) {
//					newKey = 1;
//				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
//					newKey = 2;
//				} else if (Input.GetKeyDown (KeyCode.Alpha3) && playerGears.Count >= 3) {
//					newKey = 3;
//				} else if (Input.GetKeyDown (KeyCode.Alpha4) && playerGears.Count >= 4) {
//					newKey = 4;
//				}
//				//Switch gear positions
//				Gear temp = playerGears [oldKey - 1];
//				playerGears [oldKey - 1] = playerGears [newKey - 1];
//				playerGears [newKey - 1] = temp;
//				oldKey = 0;
//				newKey = 0;
//			}
//		}
//	}

	//Gear #4 will be at the bottom, so that is the one that goes first.
	//this is for combat resolution purposes. Don't include animation code here.
	//string getGearType() {
	//	Gear nextGear = playerGears[playerGears.Count - 1];
		//return nextGear.playerAttackType;
	//}

	//character takes damage
	public void takeDamage (int amountOfDamage) {
		Debug.Log("!!! PLAYER HAS BEEN HIT !!! ");
		damaged = true;
		currentHealth -= amountOfDamage;
		healthSlider.value = currentHealth;
		//playerAudio.Play ();
		if (currentHealth <= 0 && isDead) {
			playerDeath ();
		}

		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	//character gives damage
	public void giveDamage(int _damage){
		currentHealth = currentHealth - _damage;
	}

	void playerDeath() {
		isDead = true;
		anim.SetTrigger ("Die");
		Debug.Log (playerName + "is Dead");
		playerController.enabled = false;
		//playerAudio.clip = deatchClip;
		//playerAudio.Play ();
	}

//	void deleteGear() {
		//delete gear sprite somehow

		//delete
	//	playerGears.RemoveAt(playerGears.Count - 1);
	//}

	//void timeExpired() {
	//	if (timer < 0) {
	//		isTimeExpired = true;
	//	}
	//}

	// NEED A WAY TO SET GEARS FOR EACH PLAYER 
}
