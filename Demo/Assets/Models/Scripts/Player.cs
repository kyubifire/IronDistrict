using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public thoughtBubble thought;
	public int health;
	//public GameObject healthBar;
	private int maxHealth;
	public bool canMove;
	Animator animator;
	private int state;


	// Use this for initialization
	void Start () {
		//Instantiate (healthBar);
		maxHealth = health;
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (state == 100) {
			changeState (0);
		}
			else if (state == 1) {
			state = 100;
			
		}

		if (health > maxHealth) {
			health = maxHealth;
		}

		if(canMove){
			if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x  <= 10) {
				transform.Translate (1f, 0f, 0f);

			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && transform.position.x >= -10) {
				transform.Translate (-1f, 0f, 0f);
			}
			if (! Input.GetKeyDown (KeyCode.RightArrow) && ! Input.GetKeyDown (KeyCode.LeftArrow)){
				transform.Translate (0f, 0f, 0f);

			}
		}
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		Destroy (coll.gameObject);
		health -= 5;
		//healthBar.transform.localScale =    new Vector3(((float)health / maxHealth * 20),.5f,1f);
	}

	void newThought(){
		thought.newThought ();
	}

	public void changeState(int state_){
		animator.SetInteger ("State", state_);
		state = state_;
	}
}
