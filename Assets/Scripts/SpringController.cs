using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision other) {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 10, (GetComponent<Rigidbody>().velocity.z));
		StartCoroutine (stopSpring ());
	}

	IEnumerator stopSpring() {
		yield return new WaitForSeconds (.2f);
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		yield return new WaitForSeconds (.75f);
		transform.position = new Vector3 (0.027f, -0.486f, 3.929f);
	}
}
