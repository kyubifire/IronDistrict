using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_GameManager : MonoBehaviour {
	//Tile gameTile;
	GameObject tileObj;

	//M3_Player playerSelf;
	GameObject playerObj;

	public int playerHealth;

	// Use this for initialization
	void Start () {
		
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		//Debug.Log (playerObj);

		playerHealth = playerObj.GetComponent<M3_Player>().currentHealth;
		//Debug.Log (playerHealth);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
