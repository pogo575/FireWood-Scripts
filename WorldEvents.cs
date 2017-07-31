using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slate;
using MovementEffects;

public class WorldEvents : MonoBehaviour {
	public Transform menuRoot;
	public Transform healthIndicator;
	public Transform gameOverRoot;

	public void MainScene(){

		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	public void StartGame(){
		gInput.gameOn = true;
		menuRoot.gameObject.SetActive(false);
		healthIndicator.gameObject.SetActive(true);
	}


	public void EndTheGame(){
		gInput.gameOn = false;

		gameOverRoot.gameObject.SetActive(true);
		menuRoot.gameObject.SetActive(false);
		healthIndicator.gameObject.SetActive(false);
	}
}
