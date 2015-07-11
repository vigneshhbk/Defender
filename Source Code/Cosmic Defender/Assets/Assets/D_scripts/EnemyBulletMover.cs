using UnityEngine;
using System.Collections;

public class EnemyBulletMover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().velocity = -transform.forward * speed;
	}
}
