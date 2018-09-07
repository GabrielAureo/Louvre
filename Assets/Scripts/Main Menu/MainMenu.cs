using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameStart(){
		SceneManager.LoadScene("Corridor");
	}

	public void Quit(){
		Application.Quit();
	}
}
