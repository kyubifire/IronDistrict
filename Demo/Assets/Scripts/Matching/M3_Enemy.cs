using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M3_Enemy : MonoBehaviour {
	public M3_Enemy instance;
	public int startingHealth = 50;
	public int currentHealth;
	public Animator anim;
    public bool dead;

	// Use this for initialization
	void Start () {
        dead = false;
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
            dead = true;
			anim.SetBool ("Dead", true);
			anim.SetTrigger ("Dead");
            SceneManager.LoadScene(0);
            Debug.Log ("**Enemy DIED **");
		}
	}
}
