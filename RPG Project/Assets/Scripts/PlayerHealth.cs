using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	//public AudioClip deatchClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;
	AudioSource playerAudio;
	PlayerController playerController;
	bool isDead;
	bool damaged;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//playerAudio = GetComponent<AudioSource> ();
		playerController = GetComponent<PlayerController> ();
		// set initial health of player
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;
	}
	public void takeDamage (int amountOfDamage) {
		damaged = true;
		currentHealth -= amountOfDamage;
		healthSlider.value = currentHealth;
		//playerAudio.Play ();

		if (currentHealth <= 0 && isDead) {
			playerDeath ();
		}
	}

	void playerDeath() {
		isDead = true;
//		anim.SetTrigger ("Die");
		playerController.enabled = false;
		//playerAudio.clip = deatchClip;
		//playerAudio.Play ();
	}
}