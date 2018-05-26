using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

	private float timeOfFloorBounce = 0.0f;
	private bool touchedWall = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < -1.0f) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Wall") {
			touchedWall = true;
		}
		else if (col.gameObject.tag == "Floor") {
			timeOfFloorBounce = Time.time;
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.tag == "Floor") {

			if (Time.time - timeOfFloorBounce > 2.0f) {
				Destroy (this.gameObject);
				return;
			}

			if (Time.time - timeOfFloorBounce > 1.0f) {
				this.gameObject.tag = "Untagged";
			}
		}
	}

	public int getDamage() {

		if (touchedWall) {
			return 2;
		}

		return 1;
	}
}
