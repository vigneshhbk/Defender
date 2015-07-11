using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public bool isBossFight = false;
	public bool isPaused = false;
	void Update () {
		if (isBossFight == false && isPaused == false) {
			transform.position = new Vector3 (transform.position.x + 0.005f, 10.0f, 0.0f);
		}
	}
}
