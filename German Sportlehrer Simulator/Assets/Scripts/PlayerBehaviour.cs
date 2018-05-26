using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public GameObject PathBall;
	public float maxForce;
	public float minForce;

	private float force = 2500.0f;
	private float lastSpawn = 0.0f;

	// Use this for initialization
	void Start () {
		//force = minForce;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - lastSpawn > 0.1f) {

			// Raycast um Ziel zu finden
			//RaycastHit hit;
			//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//if (Physics.Raycast (ray, out hit, 100.0f)) {
				//Debug.Log (hit.point);
			//}

			GameObject ball = Instantiate(PathBall, transform.position, Quaternion.identity);
			ball.transform.rotation = this.gameObject.transform.rotation;
			ball.GetComponent<Rigidbody> ().AddForce (ball.transform.forward * force);

			lastSpawn = Time.time;
		}
	}
}
