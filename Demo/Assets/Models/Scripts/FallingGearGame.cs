using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGearGame : MonoBehaviour {
	public int time;
	private float timeEnd;
	private double nextSpawnTime;
	public Gear falling;
	public int health;
	public GameObject healthBar; 
	// Use this for initialization
	void Start () {
		
		timeEnd = Time.time + time;
		nextSpawnTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time <= timeEnd && Time.time >= nextSpawnTime) {
			Instantiate (falling);
			nextSpawnTime += Random.Range (.5f, 1.5f);
		}
	
		
	}
}
