using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour {

	public Camera cam;
	public Rigidbody2D rb;
	public Renderer rend;
	public GameController gameController;

	private float maxWidth;
	private bool canControl;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		canControl = false;
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<Renderer>();
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = rend.bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;

	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {
		if (canControl) {
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 targetPosition = new Vector3 (rawPosition.x, -3.1f, 0.0f);
			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
			rb.MovePosition (targetPosition);
		}
	}

	public void ToggleControl(bool toggle) {
		canControl = toggle;
	}
}
