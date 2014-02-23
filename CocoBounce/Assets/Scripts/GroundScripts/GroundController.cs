using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundController : MonoBehaviour {

	Queue<GroundObject> groundObjects = new Queue<GroundObject>();

	GameplayManager parent;

	// Coordinates
	public GameObject leftBoundary;
	public GameObject rightBoundary;
	public GameObject bottomBoundary;
	public GameObject topBoundary;
	public float leftXBoundary;// = leftBoundary.transform.x;
	public float rightXBoundary;// = rightBoundary.transform.x;
	public float bottom;// = bottomBoundary.transform.y;

	public static float nextBlockCoordinate;

	// GroundObject metrics
	float width = .4f;
	float yCoordinate = -3.00f;
	float zCoordinate = -1.00f;

	float normalScaleWidth = Constants.normalScaleWidth;
	float largeWidth = Constants.largeWidth;
	float mediumWidth = Constants.mediumWidth;

	public static float velocity = 0.1f;

	static float stopVelocity = 0.0f;
	static float startVelocity = 0.1f;
	static float maxVelocity = 0.50f;
	static float minVelocity = 0.1f;

	static float acceleration = 0.05f;

	// Prefabs
	public GameObject groundObject;

	void Start(){
		InitialGroundSetup();
		velocity = stopVelocity;
	}

	public void Init(GameplayManager _parent){
		parent = _parent;
	}


	// Update is called once per frame
	void Update () {
		switch (parent.gameState){

			case GameplayManager.GameState.PreGame:
				break;
			case GameplayManager.GameState.InGame:
				UpdateVelocity();
				MoveLevelBlocks();
				GenerateLevelBlocks();
				break;
			case GameplayManager.GameState.PostGame:
				break;	
		}
	}

	void InitialGroundSetup(){	

		// Set the variables
		leftXBoundary = leftBoundary.transform.position.x;
		rightXBoundary = rightBoundary.transform.position.x;
		bottom = bottomBoundary.transform.position.y;
		nextBlockCoordinate = leftXBoundary;

		// Set up the blocks
		CreateStartingBlocks();
		GenerateLevelBlocks();

	}

	void UpdateVelocity(){

		// Get a button push + a small change so that if the user 
		// isn't pressing a button the ground still moves
		float velocityChange = Input.GetAxis("Horizontal") + acceleration;// * -1.0f;

		float possibleVelocity = velocity + (acceleration * Time.timeScale * velocityChange);

		Debug.Log("Possible Velocity: " + possibleVelocity);

		if (velocity <= minVelocity || possibleVelocity >= minVelocity && possibleVelocity <= maxVelocity){
			velocity += acceleration * Time.timeScale * velocityChange;
		}
	}

	void MoveLevelBlocks(){
		nextBlockCoordinate += velocity * Time.timeScale * -1.0f;

		if (groundObjects.Count > 0){

			while(true){
				if (groundObjects.Peek().getXPosition() <= leftXBoundary){
					groundObjects.Dequeue().Delete();
				}
				else{
					break;
				}
			}
		}
	}

	void CreateStartingBlocks(){
		float end = Constants.coconutPosition.x + width;

		for (float i = leftXBoundary; i <= end; i += width){
			AddGroundObject(i);
		}

		nextBlockCoordinate += width * 10;
	}	

	void GenerateLevelBlocks(){
		if (nextBlockCoordinate > rightXBoundary){
			return;
		}

		while (nextBlockCoordinate <= rightXBoundary){
			int groupSize = Random.Range(3, 6);
			AddBlockGroup(groupSize);

			// Add the gap
			nextBlockCoordinate += GetRandomWidth();
		}
	}

	// Add a group of GroundObjects of size groupSize
	void AddBlockGroup(int groupSize){

		for (int i = 0; i < groupSize; i++){
			AddGroundObject();
		}
	}

	// Add a GroundObject. For easy looping in AddBlockGroup. Will automatically make a block at nextBlockCoordinate and 
	// increment nextBlockCoordinate by width. 
	void AddGroundObject(){
		GameObject newObject = (GameObject) Instantiate(groundObject, new Vector3(nextBlockCoordinate, yCoordinate, zCoordinate), transform.rotation);
		GroundObject newGroundObject = newObject.GetComponent<GroundObject>();
		newGroundObject.Init(this);
		groundObjects.Enqueue(newGroundObject);

		nextBlockCoordinate += width;
	}

	// Adds a GroundObject at xCooridate. Updates nextBlockCoordinate to xCoordinate
	void AddGroundObject(float xCoordinate){
		GameObject newObject = (GameObject) Instantiate(groundObject, new Vector3(xCoordinate, yCoordinate, zCoordinate), transform.rotation);
		GroundObject newGroundObject = newObject.GetComponent<GroundObject>();
		newGroundObject.Init(this);
		groundObjects.Enqueue(newGroundObject);

		nextBlockCoordinate = xCoordinate;
	}

	float GetRandomWidth(){
		return Random.Range(0.4f * 3, 0.4f * 6);
	}
	

}
