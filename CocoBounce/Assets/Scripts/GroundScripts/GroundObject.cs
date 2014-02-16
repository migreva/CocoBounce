using UnityEngine;
using System.Collections;

public enum GroundObjectType{
	Regular = 0,
	Bouncy = 1,
	Stoppy = 2
}

public class GroundObject : MonoBehaviour {

	public GroundController parent;

	public GroundObjectType objectType;
	// public GameObject gameObject;

	// Prefab
	public GameObject GroundObjectPrefab;

	// Constants
	public float yCoordinate = -3.00f;
	public float zCoordinate = -1.00f;

	public float normalScaleWidth = Constants.normalScaleWidth;
	public float largeWidth = Constants.largeWidth;
	public float mediumWidth = Constants.mediumWidth;



	// Use this for initialization
	void Start () {
	}

	public void Init(GroundController parentObj){
		parent = parentObj;
	}

	public float getXPosition(){
		return transform.position.x;
	}

	public void Delete(){
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () { 
		transform.Translate(new Vector3((GroundController.velocity * Time.timeScale * -1.0f), 0.0f, 0.0f));
	
	}

}
