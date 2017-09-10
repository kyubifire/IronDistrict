using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
	public float moveSpeed;
	private Rigidbody2D myRigidBody;

	public bool isWalking;

	public float walkTime;
	public float waitTime;

	private float walkCounter;
	private float waitCounter;

	private int walkDirection;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();

		waitCounter = waitTime;
		walkCounter = walkTime;

		chooseDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWalking) {
			walkCounter -= Time.deltaTime;

			switch (walkDirection) {
			case 0:
				myRigidBody.velocity = new Vector2 (0, moveSpeed);
				break;
			case 1:
				myRigidBody.velocity = new Vector2 (moveSpeed, 0);
				break;

			case 2:
				myRigidBody.velocity = new Vector2 (0, -moveSpeed);
				break;

			case 3:
				myRigidBody.velocity = new Vector2 (-moveSpeed, 0);
				break;
			}

			if (walkCounter < 0) {
				isWalking = false;
				waitCounter = waitTime;
			}
		} else {
			waitCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;

			if (waitCounter < 0) {
				chooseDirection ();
			}	
		}
		
	}

	public void chooseDirection() {
		walkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}
}
