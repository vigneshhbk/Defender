using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	public GameObject bullet;
	public int speed;
	public int startWait;
	public int fireWait;
	public int nextFire;
	public int fireCount;
	public int abductDelay;
	public GameObject explosion;
	public GameObject playerExplosion;
	private float abductionTime;
	private GameObject[] humanoids;
	private GameObject humanoid;
	private bool onMission = true;
	private GameObject dummyObject;
	private GameController gameController;
	public int scoreValue;
	private ShipController shipController;
	
	void Start()
	{
		GameObject shipControllerObject = GameObject.FindWithTag ("Player");
		if (shipControllerObject != null) {
			shipController = shipControllerObject.GetComponent<ShipController>();
		}

		if (shipController == null) {
			Debug.Log("Cannot find 'ShipController' object");
		}

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}

		dummyObject = GameObject.FindGameObjectWithTag ("DummyObject");
		gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		StartCoroutine (StartFiring ());
		abductionTime = Time.time + abductDelay;
	}

	void Update()
	{
		if (Time.time > abductionTime) {
			if(onMission == true){
				humanoid = SelectHumanoid ();
				onMission = false;
			}

			Abduct ();
		}
	}

	IEnumerator StartFiring()
	{
		yield return new WaitForSeconds(startWait);
		while (true) 
		{
			for (int i = 0; i < fireCount; i++) 
			{
				Instantiate (bullet, transform.position, bullet.transform.rotation); 
				yield return new WaitForSeconds (Random.Range(0.5f, 1.5f) * fireWait);
			}
			
			yield return new WaitForSeconds(nextFire);
		}
	}

	GameObject SelectHumanoid()
	{
		humanoids = GameObject.FindGameObjectsWithTag ("Humanoid");
		GameObject humanoid = humanoids.Length > 0 ? humanoids [Random.Range (0, humanoids.Length - 1)] : null;
		if (humanoid != null) {
			humanoid.tag = "AbductedHumanoid";
		}

		return humanoid;
	}

	void Abduct()
	{
		if (humanoid != null) {
			transform.LookAt(humanoid.transform);
			transform.position += transform.forward * speed * 0.01f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") == true || other.CompareTag ("PlayerBullet") == true) {
			gameObject.GetComponent<AudioSource>().Play();
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			if (other.CompareTag ("Player")) {
				shipController.TakeDamage(gameObject);
//				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
//				gameController.GameOver();
			}
			else{
				gameController.AddScore(scoreValue);
			}
		} 
		else if (other.CompareTag ("AbductedHumanoid") == true) {
			transform.LookAt(dummyObject.transform);
			gameObject.GetComponent<Rigidbody> ().velocity = transform.forward*(5 + speed);
			other.GetComponent<Rigidbody> ().isKinematic = false;
			other.GetComponent<Rigidbody> ().velocity = transform.forward*(5 + speed);
		}
	}
}
