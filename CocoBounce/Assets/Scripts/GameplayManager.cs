using UnityEngine;
using System.Collections;

enum GameState = { PreGame, InGame, PostGame }

public class GameplayManager : MonoBehaviour {

	// Objects
	public Coconut coconut;
	public GroundController; 
	
	// State
	public GameState gameState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
