using UnityEngine;
using System.Collections;

public class MotherShip : MonoBehaviour {
	
	public GameObject bullet;
	public GameObject [] explosion;
	public Transform bulletSpawn;
	public GameObject bullet1;
	public Transform bulletSpawn1;
	public GameObject enemy;
	public Transform enemySpawn;
	public float startWait;
	public float fireWait;
	public float fireCount;
	public float nextFire;
	private GameController gameController;
	private Vector3 MovingDirection = Vector3.forward;
	private GameObject mainCamera;
	private int health;

	void Start()
	{
		mainCamera = GameObject.FindWithTag ("MainCamera");
		mainCamera.GetComponent<CameraController> ().isBossFight = true;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}

		health = gameController.bossHealth;
		StartCoroutine (StartFiring ());
	}	
	
	//for moving mothership
	void Update () {
		
		gameObject.transform.Translate(MovingDirection * Time.smoothDeltaTime);		
		if(gameObject.transform.position.z > 3){
			MovingDirection = Vector3.back;
		}else if (gameObject.transform.position.z < -3) {
			MovingDirection = Vector3.forward;
		}
	}
	
	IEnumerator StartFiring()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < fireCount; i++) {
				Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
				Instantiate (bullet1, bulletSpawn1.position, bulletSpawn1.rotation);
				Instantiate (enemy, enemySpawn.position, enemySpawn.rotation);
				yield return new WaitForSeconds (Random.Range (0.5f, 1.5f) * fireWait);
			}
				
			yield return new WaitForSeconds (nextFire);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("PlayerBullet")) 
		{
			Destroy(other.gameObject);
			health = health - 10;
			if(health <= 0)
			{
				Destroy (gameObject);	
				for(int i =0; i< explosion.Length;i++)
				{
					Instantiate(explosion[i], transform.position, transform.rotation);
				}

				gameController.BossDeath();
			}			
		}		
	}
}
