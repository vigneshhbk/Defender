using UnityEngine;
using System.Collections;

public class BoundaryMover : MonoBehaviour {
	public GameObject mainCamera;
	void Update () {
		transform.position = new Vector3 (mainCamera.transform.position.x, 0.0f, 0.0f);
	}
}
