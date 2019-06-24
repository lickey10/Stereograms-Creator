using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public float health = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		// apply collision damage
		if (collision.relativeVelocity.magnitude > 0.5) {
			health -= collision.relativeVelocity.magnitude;
		}
		
		// destroy if health is too low
		if (health <= 0) {
			Destroy(gameObject);
			
			// restart the scene if this was the last box
			GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
			if (boxes.Length <= 1) {
				Application.LoadLevel("Main");
			}
		}
	}
}
