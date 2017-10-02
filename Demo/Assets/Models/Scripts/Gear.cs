using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {


				//sets type 0 = none. 1 == green, 2 == blue, 3 == red
	//SpriteRenderer spriteRenderer;
	//Rigidbody2D rigidBody;
	Animator animator;
	Vector3 velocity;
	public GameObject player;
	public int type;	


									// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		//spriteRenderer = GetComponent<SpriteRenderer> ();
		//rigidBody = GetComponent<Rigidbody2D> ();


		transform.position = new Vector3(Random.Range (-10, 10), 7, -1);		//sets initial position to a random position just above the screen
		velocity =  new Vector3(0, (int)Random.Range (-3, -1), 0);				//sets the velocity to a random number downwards
		type = (int)(Random.Range(0,4));
		animator.SetInteger ("State", type);


	}
	
	// Update is called once per frame
	void Update () {
		//rigidBody.


		transform.Translate(velocity * Time.deltaTime);
		if (transform.position.y <= -5) {
			Destroy (gameObject);
		}
		
	}


}
