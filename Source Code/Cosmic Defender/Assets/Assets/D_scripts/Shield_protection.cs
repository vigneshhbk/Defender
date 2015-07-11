using UnityEngine;
using System.Collections;

public class Shield_protection : MonoBehaviour {
	public GameObject child;
	public GameObject explosion;
	private int life = 3;
	private int flag = 0;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("power1")) 
		{
			Destroy (other.gameObject);	
			StartCoroutine (ShieldStart ()); 
		}

		if (other.gameObject.CompareTag ("EnemyBullet") || other.gameObject.CompareTag ("Enemy")) 
		{
			Destroy (other.gameObject);	
			if (flag == 1) 
			{
				child.SetActive(false); 
				flag =0;
			}
			else
			{
				life -=1;
				if (life == 0) {
					Destroy (gameObject);
					Instantiate (explosion, transform.position, transform.rotation);
				}
				else 
				{
				StartCoroutine (KillObject());
				}
			}
		}
	}

	IEnumerator ShieldStart ()
	{
		flag = 1;
		child.SetActive(true);
		yield return new WaitForSeconds (10);
		flag = 0;
		child.SetActive(false);
	}

	IEnumerator KillObject ()
	{
		gameObject.SetActive(false);
		Instantiate (explosion, transform.position, transform.rotation);
		gameObject.SetActive (true);
		yield return new WaitForSeconds (10);
	}

}