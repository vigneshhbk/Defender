using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary1
{
	public float xMin, xMax, zMin, zMax;
}
public class NewBehaviourScript : MonoBehaviour {
	public float speed;
	public Boundary1 boundary;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.UpArrow))
		{ 
			//transform.Translate(Vector3.left * speed * Time.deltaTime);
			transform.position = Vector3.Lerp(transform.position,transform.TransformPoint(Vector3.left),speed* Time.deltaTime); 
			
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position = Vector3.Lerp(transform.position,transform.TransformPoint(Vector3.right),speed* Time.deltaTime); 
			
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = Vector3.Lerp(transform.position, transform.TransformPoint (Vector3.back),speed* Time.deltaTime); 
			
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{ 
			transform.position = Vector3.Lerp(transform.position,transform.TransformPoint(Vector3.forward),speed* Time.deltaTime);
		}
		
		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
				);
	}
}
