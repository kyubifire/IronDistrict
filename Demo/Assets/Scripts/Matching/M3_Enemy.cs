using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_Enemy : MonoBehaviour {
	public M3_Enemy instance;
	public int startingHealth = 100;
	public int currentHealth;
	public Animator anim;

	// Use this for initialization
	void Start () {
		instance = GetComponent<M3_Enemy> ();
		anim = GetComponent<Animator> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage (int amountOfDamage) {
		currentHealth -= amountOfDamage;
		Debug.Log ("Enemy Health: " + currentHealth);
		if (currentHealth <= 0) {
			anim.SetBool ("Dead", true);
			anim.SetTrigger ("Dead");
			Debug.Log ("**Enemy DIED **");
		}
	}
}
