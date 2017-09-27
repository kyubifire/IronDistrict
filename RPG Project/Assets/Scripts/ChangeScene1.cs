using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            changeToScene(1);
        }
    }

	public void changeToScene(int sceneNumber)
	{
		SceneManager.LoadScene (sceneNumber);
	}
}
