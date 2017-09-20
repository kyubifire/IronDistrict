using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {
	public List<Gear> enemyGears;
	bool canSwitch;
	bool isSwitched;
	private bool canBattle;
	private bool startBattle;

	public enum enemyState {
		idle, 
		changeGear, 
		selectGear,
		dead
	}
	//private enemyState currentState = enemyState.idle;

	//public enemyState state {
		// create getter & setter to reference enemyState value
		//get {
		//	return currentState;
		//}
		//set {
			//state = value;
			//StopAllCoroutines ();


			//switch (currentState) {
			//case enemyState.idle:
			//	StartCoroutine (Enemy_Idle ());
				//Debug.Log ("Enemy is idle");
				//break;
		//	case enemyState.changeGear:
				//StartCoroutine (Enemy_changeGear ());
				//Debug.Log ("Enemy is switching gears");
				//break;
			//case enemyState.selectGear:
				//StartCoroutine (Enemy_selectGear ());
				//Debug.Log ("Enemy has selected a gear");
				//break;
			//case enemyState.dead:
				//StartCoroutine (Enemy_Dead ());
				//Debug.Log ("Enemy is dead");
				//break;
			//}
		//}
	//}

	// Use this for initialization
	//void Start () {
		//state = enemyState.idle;
	//	isSwitched = false;
		//canSwitch = true;
	//	canBattle = true;
	//	startBattle = false;
	//}
	
	// Update is called once per frame
	//void Update () {
	//}
		// If player presses 'Enter' key, start battle sequence
		//if (Input.GetKeyDown (KeyCode.Return) && canBattle == true) {
		//	canBattle = false;
		//	matchStart ();
		//}

		// make sure gear switching is enable and time is still available
		//if (canSwitch == true) {
			// allow enemy to switch gears
			//state = enemyState.changeGear;
		//}
	//}

	//public void matchStart() {
		// disable gear switching
	//	canSwitch = false;
	//}

	//public IEnumerator Enemy_Idle() {

	//}

	//public IEnumerator Enemy_changeGear() {
		
	//}

	//public IEnumerator Enemy_selectGear() {

	//}

	//public IEnumerator Enemy_Dead() {

	//}

	//public void setStates (enemyState s) {
	//}
}
