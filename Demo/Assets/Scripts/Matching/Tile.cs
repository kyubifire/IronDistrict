using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	public static Tile instance;
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;
	private bool tileShifting = false;
	public bool matchFound = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	//*********************************************************************\\
//	public GameObject healthBar;
//	private GameObject healthGauge;
//
//	public GameObject enemyBar;
//	private GameObject enemyGauge;

	public int playerHealth;
	public int enemyHealth;

	GameObject playerObj;
	GameObject enemyObj;

	Animator playerAnim;
	Animator enemyAnim;

	//Animator anim;

	public int playerAttackDamage;
	public int enemyAttackDamage;

	public int maxDamage;

	// After getting matches back to back, player can get a multiplier value added to attackDamage
	public int multiplier;
	// After 3 swaps and no matches, enemy attacks -> penalty for player
	public int numSwaps;
	public bool canAttack;
	public bool enemyAttacking;

	void Awake() {
		render = GetComponent<SpriteRenderer>();
    }

	void Start () {
		instance = GetComponent<Tile> ();
	
		canAttack = false;
		enemyAttacking = false;
		maxDamage = 5;
		multiplier = 0;
		playerAttackDamage = 0;
		enemyAttackDamage = 5;

//		healthGauge = (GameObject)Instantiate (healthBar);
//		enemyGauge = (GameObject)Instantiate (enemyBar);

		playerObj = GameObject.FindGameObjectWithTag ("Player");
		//Debug.Log (playerObj);
		enemyObj = GameObject.FindGameObjectWithTag("Enemy");
		//Debug.Log (enemyObj);

		playerAnim = playerObj.GetComponent<M3_Player>().anim;
		enemyAnim = enemyObj.GetComponent<M3_Enemy> ().anim;

		playerHealth = playerObj.GetComponent<M3_Player>().currentHealth;
		//Debug.Log (playerHealth);
		enemyHealth = enemyObj.GetComponent<M3_Enemy>().currentHealth;
		//Debug.Log ("Enemy Health: " + enemyHealth);
	}

	void Update() {
		checkHealth ();
	}

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
		//Debug.Log ("Selected tile: " + previousSelected);
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	// "Queries Start in Colliders" must be uncheck.
	// Edit -> Project Settings -> Physics 2D -> UNCHECK Queries Start in Colliders
	void OnMouseDown() {
		// not selectable conditions
		if (render.sprite == null || BoardManager.instance.IsShifting) {
			return;
		}

		if (isSelected) {
			Deselect ();
		} else {
			if (previousSelected == null) {
				Select ();
			} else {
				if (GetAllAdjacentTiles ().Contains (previousSelected.gameObject)) {
					SwapSprite (previousSelected.render);
					previousSelected.ClearAllMatches ();
					previousSelected.Deselect ();
					ClearAllMatches ();
				} else {
					previousSelected.GetComponent<Tile> ().Deselect ();
					Select ();
				}
			}

		}
	}
		
	public void SwapSprite(SpriteRenderer render2) {
		//Debug.Log (" ** IN SWAPSPRITE **");
		if (render.sprite == render2.sprite) {
			return;
		}
		Sprite tempSprite = render2.sprite;
		render2.sprite = render.sprite;
		render.sprite = tempSprite;

		//Debug.Log("** NEW SPRITE: " + render.sprite);
		numSwaps += 1;
		Debug.Log ("Num Swaps: " + numSwaps);
		if (numSwaps >= 3) {
			StartAttack ();
		}
	}

	private GameObject GetAdjacent(Vector2 castDir) {
		//Debug.Log ("** IN GET ADJACENT **");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		//Debug.Log (hit.collider);
		if (hit.collider != null) {
			//Debug.Log ("Collider not null: " + hit.collider.gameObject);
			return hit.collider.gameObject;
		}
		return null;
	}

	private List<GameObject> GetAllAdjacentTiles() {
		//Debug.Log (" ** IN GET ALL ADJACENT TILES");
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++) {
			//Debug.Log("Adjacent Directions: " + adjacentDirections[i]);
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		//Debug.Log ("Adjacent tiles: " + adjacentTiles);
		return adjacentTiles;
	}
		
	private List<GameObject> FindMatch(Vector2 castDir) {
		List<GameObject> matchingTiles = new List<GameObject>();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite) {
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles;
	}

	private void ClearMatch(Vector2[] paths) {
		List<GameObject> matchingTiles = new List<GameObject>();
		for (int i = 0; i < paths.Length; i++) { 
			matchingTiles.AddRange(FindMatch(paths[i])); 
		}
		if (matchingTiles.Count >= 2) {
			for (int i = 0; i < matchingTiles.Count; i++) {
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true;
			multiplier += 1;
			canAttack = true;
		}

		if (matchFound == true) {
			StartAttack ();
		}
	}

	public void ClearAllMatches() {
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound) {
			render.sprite = null;
			matchFound = false;
			StopCoroutine(BoardManager.instance.FindNullTiles());
			StartCoroutine(BoardManager.instance.FindNullTiles());
		}

		tileShifting = false;

//		if (canAttack == true && tileShifting == false) {
//			StartAttack();
//		}
	}

	public void StartAttack() {
		Debug.Log ("IN START ATTACK");
		Debug.Log ("can player attack? " + canAttack);
		Debug.Log ("was there a match found? " + matchFound);
		if (canAttack == true) {
			Debug.Log ("PLAYER ATTACKING!!");
			playerAttackDamage += maxDamage;
			//playerObj.GetComponent<M3_Player> ().anim.SetBool ("IsAttacking", true);
			playerAnim.SetBool ("IsAttacking", true);
			enemyHealth -= playerAttackDamage;
			Debug.Log ("Enemy Health: " + enemyHealth);
			//if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Breya_Attacking")) {
			//	StopAttack();
			//}
		} else if (numSwaps >= 3 && matchFound == false) {
			Debug.Log ("ENEMY ATTACKING!!");
			enemyAttacking = true;
			enemyAnim.SetBool ("IsAttacking", true);
			enemyAnim.SetTrigger ("IsAttacking");
			playerHealth -= enemyAttackDamage;
			Debug.Log ("Player Health: " + playerHealth);
		}

		Invoke ("StopAttack", 1);
	}

	public void StopAttack() {
		if (canAttack == true) {
		Debug.Log ("STOPPING ATTACK");
		//playerObj.GetComponent<M3_Player> ().anim.SetBool ("IsAttacking", false);
		playerAnim.SetBool("IsAttacking", false);
		canAttack = false;
		tileShifting = true;
		} else if (enemyAttacking == true) {
			enemyAttacking = false;
			enemyAnim.SetBool ("IsAttacking", false);
		}
		//checkHealth ();
	}

	void checkHealth() {
		if (playerHealth <= 0) {
			playerAnim.SetBool ("Dead", true);
		}

		if (enemyHealth <= 0) {
			enemyAnim.SetBool ("Dead", true);
		}
	}
}