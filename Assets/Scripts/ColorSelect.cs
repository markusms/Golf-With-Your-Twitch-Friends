using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSelect : MonoBehaviour {

	public static string selectedColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickedGlassButton() {
		selectedColor = "glass";
		SceneManager.LoadScene ("Hole1");
	}

	public void clickedMetalButton() {
		selectedColor = "metal";
		SceneManager.LoadScene ("Hole1");
	}

	public void clickedYellowButton() {
		selectedColor = "yellow";
		SceneManager.LoadScene ("Hole1");
	}
}
