using UnityEngine;
using System.Collections;

public class RockMover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<Rigidbody> ().velocity = -transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
	}
}
