using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class ShipController : MonoBehaviour
{
	public float speed;
	public Boundary boundary;
	public GameObject bullet;
	public Transform bulletSpawn;
	public GameObject bullet1;
	public Transform bulletSpawn1;
	public GameObject bullet2;
	public Transform bulletSpawn2;
	public GameObject bullet3;
	public Transform bulletSpawn3;
	public float shotRate;
	private float nextShot;
	private int flag = 0;
	private Rigidbody rigidBody;
	public GameObject mainCamera;
	private int bulletlimit = 3;
	public GameObject playerExplosion;
	private GameController gameController;
	public GameObject child;
	public GameObject explosion;
	private bool isShieldActive = false;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}

		rigidBody = gameObject.GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Space) && Time.time > nextShot)
		{
			if(flag ==1)
			{
				nextShot = Time.time + shotRate;
				Instantiate(bullet1, bulletSpawn1.position, bulletSpawn1.rotation);
				Instantiate(bullet2, bulletSpawn2.position, bulletSpawn2.rotation);
				Instantiate(bullet3, bulletSpawn3.position, bulletSpawn3.rotation);
				bulletlimit -=1;
				if(bulletlimit==0)
				{
					flag =0;
				}
			}
			else
			{
				nextShot = Time.time + shotRate;
				Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
			}

			gameObject.GetComponent<AudioSource>().Play();
		}

		if (Input.GetKey (KeyCode.C)) 
		{
			if(flag==0 && bulletlimit > 0)				
			{
				flag =1;
			}
			else
			{
				flag =0;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("power")) {
			//bullet = bullet1;
			//bulletSpawn = bulletSpawn1;
			flag = 1;
			Destroy (other.gameObject);
		} else if(other.gameObject.CompareTag ("EnemyBullet")){
			TakeDamage(other.gameObject);
		}

		if (other.gameObject.CompareTag ("power1")) 
		{
			Destroy (other.gameObject);	
			StartCoroutine(ShieldStart ()); 
		}

		if (other.gameObject.CompareTag ("rock")) 
		{
			Destroy (gameObject);
		} 
	}

	IEnumerator ShieldStart ()
	{
		isShieldActive = true;
		child.SetActive(true);
		yield return new WaitForSeconds (10);
		isShieldActive = false;
		child.SetActive(false);
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidBody.velocity = movement * speed;
		rigidBody.position = new Vector3
				(
					Mathf.Clamp (rigidBody.position.x, mainCamera.transform.position.x + boundary.xMin, mainCamera.transform.position.x + boundary.xMax),
					0.0f,
					Mathf.Clamp (rigidBody.position.z, boundary.zMin, boundary.zMax)
				);
	}

	public void TakeDamage(GameObject other){
		Destroy (other);
		if (isShieldActive == false) {
			Destroy (gameObject);
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		} else {
			RemoveShield();
		}
	}

	void RemoveShield(){
		isShieldActive = false;
		child.SetActive (false);
	}
}