using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	// call EnemyHealth class to get health value
	public PlayerHealth currentHealth;

	//public string allyName;
	public int maxDamage;
	public int currentDamage;
	public string attackType;

	public bool isAlive;
	private bool isIncapacitated;

	Animator animator;
	SpriteRenderer spRen;

	// Use this for initialization
	void Start () {
		currentDamage = maxDamage;
		isAlive = true;
		animator = GetComponent<Animator>();
		spRen = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		//Checks if enemy is dead
		if (currentHealth <= 0) {
			animator.SetBool ("Dead", true);
		}
		//checks if stunned or sleep, etc
		// else if (isIncapacitated) {}
		//if all good
		else {
		
		}
	}

	//if enemy takes damage
	public void damage(int _damage){
		currentHealth = currentHealth - _damage;
		//checks for death
		if(currentHealth <= 0) {
			death ();
		}
	}
	void death(){
		//Debug.Log (allyName + "is Dead");
		isAlive = false;
	}
}