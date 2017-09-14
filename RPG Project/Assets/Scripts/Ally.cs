using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ally : MonoBehaviour {
	
	Animator animator;
	SpriteRenderer spRen;
	public string allyName;
	public int maxDamage;
	public int maxHealth;
	public string attackType;
	public int currentHealth;
	public int currentDamage;
	public bool isAlive;
	//private bool isIncapacitated;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentDamage = maxDamage;
		isAlive = true;
		animator = GetComponent<Animator>();
		spRen = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	//Checks if ally is dead
		if (currentHealth <= 0) {
			animator.SetBool ("Dead", true);
		}

	//checks if stunned or sleep, etc
		// else if (isIncapacitated) {}

	//if all good
		else {
			
		}
		
	}
	//character takes damage
	public void damage(int _damage){
		currentHealth = currentHealth - _damage;

	//checks for death
		if(currentHealth <= 0) {
			ally_death ();

		}
	}

	void ally_death(){
		Debug.Log (allyName + "is Dead");
		isAlive = false;
	}
}
