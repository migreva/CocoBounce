using UnityEngine;
using System.Collections;



public class GameplayManager : MonoBehaviour {

	public enum GameState { PreGame, InGame, PostGame };

	public GroundController groundController;

	// State
	public GameState gameState;

	// Time stuff
	float time = 0.0f;

	float startDelay = 3.0f;

	// Prefabs
	public GameObject p_groundController; 

	// Use this for initialization
	void Start () {
		gameState = GameState.PreGame;

		// Make the GroundController
		GameObject tmp = (GameObject) Instantiate(p_groundController, new Vector3(0, 0, 0), transform.rotation);
		groundController = tmp.GetComponent<GroundController>();
		groundController.Init(this);
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState){
			case GameState.PreGame:

				if (time >= startDelay){
					Debug.Log("Changing state!!");
					gameState = GameState.InGame;
				}
				break;
			case GameState.InGame:
				break;
			case GameState.PostGame:
				break;
		}

		time += Time.deltaTime;
	}
}
