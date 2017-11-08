using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject[] fruits;
	public int fails;
	public Text failsText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject startButton;
	public Failure failure;
	public BucketController bucketController;

	private float maxWidth;

	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float fruitWidth = fruits[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - fruitWidth;
	}

	public void StartGame() {
		StartCoroutine (Spawn());
		startButton.SetActive (false);
		bucketController.ToggleControl (true);
	}

	void speedUp() {
		foreach (GameObject fruit in fruits) {
			fruit.GetComponent<Rigidbody2D> ().gravityScale += 0.2f;
		}
	}

	IEnumerator Spawn() {
		foreach (GameObject fruit in fruits) {
			fruit.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
		while (failure.getFails() < 5) {
			GameObject fruit = fruits [Random.Range (0, fruits.Length)]; 
			speedUp ();
			Vector3 spawnPosition = new Vector3 (
				                       Random.Range (-maxWidth, maxWidth), 
				                       transform.position.y, 
				                       0.0f
			                       );
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (fruit, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));
		}
		gameOverText.SetActive (true);
		restartButton.SetActive (true);
	}

}
