using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutterController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<ConstantForce> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) { //Fire1 predefined for left mouse button
			GetComponent<ConstantForce> ().enabled = true;
		}
	}

	void OnCollisionEnter(Collision other) {
		Destroy (gameObject);
	}
}
