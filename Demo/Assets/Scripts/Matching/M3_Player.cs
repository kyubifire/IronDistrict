using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class M3_Player : MonoBehaviour {
	public M3_Player instance;

	// amount of health player starts out with
	public int startingHealth = 100;
	// player's health at a given moment	
	public int currentHealth;						

	public Animator anim;
	public int playerDamage;

	// Use this for initialization
	void Start () {
		instance = GetComponent<M3_Player> ();
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage (int amountOfDamage) {
		currentHealth -= amountOfDamage;
		Debug.Log ("Player Health: " + currentHealth);
		if (currentHealth <= 0) {
			anim.SetBool ("Dead", true);
			anim.SetTrigger ("Dead");
			Debug.Log ("**PLAYER DIED **");
		}
	}
}
