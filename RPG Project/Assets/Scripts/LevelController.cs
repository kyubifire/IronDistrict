using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	public int index;
	//public string levelName;

	public Image blackImage;
	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			// load level by build index. Scene name can also be used too.
			SceneManager.LoadScene(index);
			// SceneManager.LoadScene(levelName);
			StartCoroutine (Fading ());
			//or
			//StartCoroutiner("Fading");
		}
	}
	IEnumerator Fading() {
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => blackImage.color.a == 1);
		SceneManager.LoadScene (index);
	}
}
