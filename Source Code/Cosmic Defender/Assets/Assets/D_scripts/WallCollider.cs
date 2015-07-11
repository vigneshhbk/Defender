using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

	public void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}