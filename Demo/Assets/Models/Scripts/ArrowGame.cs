using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGame : MonoBehaviour {
										
									//time things, like end time and next create time, and how long a round is
	private float timeEnd;
	private float nextSpawnTime;
	public int time;


									//player/enemy health and their max health
	public int health;
	private int maxHealth;


	private int totalCards;					//This is the stuff that track the cards 
	private int correctCards;				//spawnded per round and how many you got right


	public int enemyHealth;
	private int enemyMaxHealth;


	public ArrowCard aCard;
										//queue for arrow cards
	public List<ArrowCard> arrows;

										//Set enemy damage,health
	public int enemyDamage;
										//your "attack" damage
	public int attackDamage;

										//create the health bar
	public GameObject healthBar;		//stores the health bar prefab
	public GameObject attackBar;		//store the attack bar prefab
	public GameObject enemyBar;
	public GameObject HUD;
	public GameObject backgrounds;
	public GameObject instructionAccess;
	public GameObject Victory;
	public GameObject Defeat;

	public Player playerAccess;
	public Enemy enemyAccess;
	private Player player;
	private Enemy enemy;

	private GameObject healthGauge;		//health bar obj
	private GameObject attackGauge;		// attack bar obj
	private GameObject enemyGauge;
	private GameObject instructions;

	//checks if you are in game
	private bool inGame;



	//tracks how many hits in a row and how much your gauge has charged
	public int consecutiveHits;
	public int attackPoints;



	// Use this for initialization
	void Start () {
		

		attackPoints = 0;
		totalCards = 0;
		correctCards = 0;
		//this sets how long befor the game stops instantiating arrows
		timeEnd = Time.time + time;

		maxHealth = health;				//set player and enemy max health to initial health
		enemyMaxHealth = enemyHealth;
		nextSpawnTime = Time.time;


		Instantiate (backgrounds);	
		Instantiate (HUD);	

		//Create the enemy and player
		enemy = (Enemy)Instantiate (enemyAccess);
		player = (Player)Instantiate (playerAccess);//create the bars for your and enemy health
		healthGauge = (GameObject)Instantiate (healthBar);
		attackGauge = (GameObject)Instantiate (attackBar);
		enemyGauge = (GameObject)Instantiate (enemyBar);
		instructions = (GameObject)Instantiate(instructionAccess);


	}
	
	// Update is called once per frame
	void Update () {
		
										//when the attack bar is full you attack and it resets the bar
		if (attackPoints >= 50) {
			attackPoints = 0;
			enemyHealth -= attackDamage;
			consecutiveHits = 1;
			//set breya animation to attack
			player.changeState (1);
		} 

		attackGauge.transform.localScale = new Vector3 (  (float)attackPoints / 6f, .5f , 1f);


		//if you are still alive
		if (health > 0) {
			healthGauge.transform.localScale = new Vector3 ((float)7.5 * health / maxHealth, .5f, 1f);
		} else {
			
			//GAME OVER DEATH
			Instantiate(Defeat);
			healthGauge.transform.localScale = new Vector3 ((float)7.5 * health / maxHealth, .5f, 1f);
			timeEnd = Time.time - 1;
			int y = 0;
			while (y < arrows.Count) {
				Destroy (arrows [0].gameObject);
				arrows.RemoveAt (0);
				y++;
			}
			if (Time.time > timeEnd + 3) {
				player.changeState (66);
			}
		}


		if (enemyHealth > 0) {
			enemyGauge.transform.localScale = new Vector3 (((float)12.4 * enemyHealth / enemyMaxHealth), .5f, 1f);
		
		} else {
			
			//Victory
			Instantiate(Victory);
			enemy.changeState (66);
			enemyGauge.transform.localScale = new Vector3 (((float)12.4 * enemyHealth / enemyMaxHealth), .5f, 1f);


			timeEnd = Time.time - 1;
			int y = 0;
			while (y < arrows.Count) {
				Destroy (arrows [0].gameObject);
				arrows.RemoveAt (0);
				y++;
			}

		}
			

		//THIIS IS EWHERE WE CREATE CARDS

		if (Time.time <= timeEnd && Time.time >= nextSpawnTime) {		//every round within the time limit it will spawn a arrow card
			inGame = true;

			arrows.Add ((ArrowCard)Instantiate (aCard));			//instantiates the arrow card prefan
			nextSpawnTime += (.4f);									// sets the next interval that it spawns
			totalCards += 1;
		
		} else {
			if (Time.time > timeEnd + 3) {
				inGame = false;
			}
		}

		//display instructions after a round
		if (Time.time > timeEnd + 3 && health > 0 && enemyHealth > 0) {
			instructions.transform.position = new Vector3 (instructions.transform.position.x, instructions.transform.position.y, -8);
		}


		//after the round ends, press ENTER to start a new round

		if (inGame == false && Input.GetKeyDown (KeyCode.Return) && health > 0 && enemyHealth > 0 ) {
			timeEnd = Time.time + time;
			nextSpawnTime =Time.time + (.3f);
			totalCards = 0;
			correctCards = 0;
			instructions.transform.position = new Vector3 (instructions.transform.position.x, instructions.transform.position.y, 20);
		}


								/*\\----------------------------
								 * -----------------------------
								 *----- IMPORTANT: 	------------
								 *-----  UP == 0	------------
								 *-----  Down == 1	------------
								 *-----  Left == 2	------------
								 *-----  Right == 3	------------
								 * -----------------------------
								 * ----------------------------
								*/

			if(Input.GetKeyDown(KeyCode.UpArrow)){						//checks the card and input
				if (arrows.Count >= 1) {								//if the top of the queue matches input
				if (arrows [0].type == 0) {								//success, else damages you
					correctInput();
				} else {
					badInput ();
				}
				}
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				if (arrows.Count >= 1) {
					if (arrows [0].type == 1) {
					correctInput ();
					} else {
					badInput ();
				}
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (arrows.Count >= 1) {
					if (arrows [0].type == 2) {
					correctInput ();
					} else {
					badInput ();
				}
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (arrows.Count >= 1) {
					if (arrows [0].type == 3) {
					correctInput ();
					} else {
					badInput ();	
						}
				}
			}


		if (arrows.Count >= 1) {
			if (arrows [0].transform.position.x <= -3.5) {
				badInput ();
				int y = 0;
				while (y < arrows.Count) {
					Destroy (arrows [0].gameObject);
					arrows.RemoveAt (0);
					y++;
				}
				consecutiveHits = 0;
				attackPoints = 0;

			}
		}
	}

											//what happens if you get it right
	void correctInput(){
		Destroy (arrows [0].gameObject);
		arrows.RemoveAt (0);
		consecutiveHits += 1;
		attackPoints += consecutiveHits;
		correctCards += 1;

	}
											//what happens when you get it wrong

	void badInput(){
		Destroy (arrows [0].gameObject);
		arrows.RemoveAt (0);
		health -= enemyDamage;
		consecutiveHits = 0;
		enemy.changeState (1);
	}
}