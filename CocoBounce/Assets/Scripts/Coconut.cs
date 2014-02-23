using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//float radius = box.Bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log("Collided with " + collision.gameObject.name);
		if (collision.gameObject.name == Constants.GroundObjectName && rigidbody.velocity.y < 0.0f){

			// Get the velocity of the coconut, and add a force that's slighty
			// less in the opposite direction to mimic a bounce
			Vector3 coconutVelocity = rigidbody.velocity;

			// If the speed is small enough, make the velocity be nothing and disable gravity 
			// will take this out eventually
			if (Mathf.Abs(coconutVelocity.y) < 1.0f){
				rigidbody.useGravity = false;
				rigidbody.velocity = new Vector3(0, 0, 0);
			}
			else{
				rigidbody.velocity = coconutVelocity * -0.98f;

			}

			//Time.timeScale = 0;
		}
	}
}
