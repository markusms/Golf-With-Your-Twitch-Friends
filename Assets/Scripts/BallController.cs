using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Globalization;

[RequireComponent(typeof(TwitchIRC))]
public class BallController : MonoBehaviour {

	private TwitchIRC IRC;
	public Transform clubObject;
	public Transform arrowObject;
	public float zForce = 100;
	public bool sandtrapped = false;
	public Transform sandSprayObject;
	public bool isJumping = false;
	public Material glassMaterial;
	public Material metalMaterial;
	public Material yellowMaterial;
	private GameObject cup;
	private bool cameraZooming = false;
	private Vector3 zoomVectorOriginal = new Vector3(0.0f, 0.0f, 0.001f);
	private Vector3 zoomVectorTarget;

	// Use this for initialization
	void Start () {
		cup = GameObject.Find ("Cup");
		IRC = FindObjectOfType<TwitchIRC>();
		//IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
		IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);

		if (ColorSelect.selectedColor == "glass") {
			GetComponent<Renderer> ().material = glassMaterial;
		}
		if (ColorSelect.selectedColor == "metal") {
			GetComponent<Renderer> ().material = metalMaterial;
		}
		if (ColorSelect.selectedColor == "yellow") {
			GetComponent<Renderer> ().material = yellowMaterial;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			GameController.currentStrokes++;
			GameController.totalStrokes++;
			Debug.Log (GameController.currentStrokes + "  " + GameController.totalStrokes);
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, zForce);
			if (sandtrapped == true) {
				Instantiate (sandSprayObject, transform.position, sandSprayObject.rotation);
			}
		}

		if (Input.GetButtonDown ("Fire2") && isJumping == false) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 3, (GetComponent<Rigidbody>().velocity.z));
			isJumping = true;
		}

		if (Input.GetKeyDown ("space")) {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			//			Instantiate (clubObject, transform.position, clubObject.rotation);
			GetComponent<Transform>().eulerAngles = Vector3.zero;
			arrowObject.GetComponent<Transform> ().position = transform.position;
			isJumping = false;
		}

		if (Input.GetKey ("a")) {
			transform.Rotate (0, -1, 0);
		}

		if (Input.GetKey ("d")) {
			transform.Rotate (0, 1, 0);
		}

		if (Input.GetKey ("w") && zForce < 1200) {
			zForce += 5;
		}

		if (Input.GetKey ("s") && zForce > 20) {
			zForce -= 5;
		}

		if (Input.GetKey ("z")) {
			cameraZooming = true;
		}

		if (cameraZooming) {
			this.cameraZoom ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Cup") {
			Debug.Log ("Completed");
			GameController.currentStrokes = 0;
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			StartCoroutine (delayLoad ());
		}

		if (other.name == "Sand") {
			sandtrapped = true;
			GetComponent<Rigidbody> ().drag = 5;
		}

		if (other.name == "PortalIn") {
			transform.position = new Vector3 (-4.35f, 0.6f, 8.49f);
			GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.z, GetComponent<Rigidbody> ().velocity.y, -GetComponent<Rigidbody> ().velocity.x);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name == "Sand") {
			sandtrapped = false;
			GetComponent<Rigidbody> ().drag = 1;
		}
	}

	IEnumerator delayLoad() {
		cameraZooming = true;
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("Hole2");
	}

	//when message is recieved from IRC-server or our own message.
	void OnChatMsgRecieved(string msg)
	{
		//parse from buffer.
		int msgIndex = msg.IndexOf("PRIVMSG #");
		string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
		string user = msg.Substring(1, msg.IndexOf('!') - 1);

		if (msgString.Contains ("!hit")) {
			chatHitBall (user, msgString.Split(' ')[1]);
		}
	}

	private void chatHitBall(string user, string message) {
		//		Debug.Log ("Hit message: " + message);
		//		Debug.Log ("power0: " + message.Split ('P') [0]);
		//		Debug.Log ("power1: " + message.Split ('P') [1]);
		//		Debug.Log ("direction0: " + message.Split ('D') [0]);
		//		Debug.Log ("direction1: " + message.Split ('D') [1]);
		try {
			float power = float.Parse(message[1].ToString(), CultureInfo.InvariantCulture.NumberFormat);
			Debug.Log ("float power: " + power);
			float direction = float.Parse(message[3].ToString(), CultureInfo.InvariantCulture.NumberFormat);
			Debug.Log ("float direction: " + direction);
			transform.Rotate (0, 30, 0);
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, power*100f);
		}
		catch (System.Exception e) {
			Debug.Log ("Error: " + e);
		}
	}

	/// <summary>
	/// Camera zoom to the cup when the game ends.
	/// </summary>
	private void cameraZoom()
	{
		zoomVectorTarget = Camera.main.WorldToViewportPoint(cup.transform.position);
		Camera.main.transform.Translate (zoomVectorOriginal);
		zoomVectorTarget.z = Mathf.Abs (Camera.main.transform.position.z - cup.transform.position.z);
		zoomVectorTarget = Camera.main.ViewportToWorldPoint(zoomVectorOriginal);
		Camera.main.transform.Translate (cup.transform.position - zoomVectorOriginal);
	}
}
