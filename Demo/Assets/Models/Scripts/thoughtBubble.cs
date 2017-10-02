using System.Collections.Generic;
using UnityEngine;

public class thoughtBubble : MonoBehaviour {
	public int type;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetInteger ("State", type);
	}


	public void newThought(){
		
		type = (int)Random.Range (1, 4);
	}
}
