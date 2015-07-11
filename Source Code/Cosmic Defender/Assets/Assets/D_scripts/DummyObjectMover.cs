using UnityEngine;
using System.Collections;

public class DummyObjectMover : MonoBehaviour {
	public GameObject mainCamera;
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(mainCamera.transform.position.x + 18.0f, 0.0f, 0.0f);
	}
}
