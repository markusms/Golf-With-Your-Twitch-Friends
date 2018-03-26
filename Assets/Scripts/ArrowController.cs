using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	public float yScale = 30;
	public float forceColor = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("a")) {
			transform.Rotate (0, 0, 1);
		}

		if (Input.GetKey ("d")) {
			transform.Rotate (0, 0, -1);
		}

		if (Input.GetKey ("w")) {
			yScale += .25f;
			forceColor -= 0.01f;
			GetComponent<Transform> ().localScale = new Vector3 (3, yScale, 1);
			GetComponent<SpriteRenderer> ().color = new Color (1, forceColor, forceColor);

		}

		if (Input.GetKey ("s")) {
			yScale -= .25f;
			forceColor += 0.01f;
			GetComponent<Transform> ().localScale = new Vector3 (3, yScale, 1);
			GetComponent<SpriteRenderer> ().color = new Color (1, forceColor, forceColor);
		}

		if (Input.GetKeyDown ("space")) {
			GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 0);
		}
	}
}
