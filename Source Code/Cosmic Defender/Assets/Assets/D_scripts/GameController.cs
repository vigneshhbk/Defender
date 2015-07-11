using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Obstacle
{
	public GameObject obstacle;
	public Vector3 spawnValues;
	public int obstacleCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
}

public class GameController : MonoBehaviour {
	public Obstacle enemyShip = new Obstacle();
	public Obstacle movingRock = new Obstacle();
	public Obstacle humanoid = new Obstacle();
	public GameObject motherShip;
	public float bossFightDelay;
	public GameObject mainCamera;
	private bool isBossFight = true;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	private bool isGameOver;
	private bool isRestart;
	private int score;
	public int bossHealth;
	public GameObject shield;
	public GameObject power;
	private float shieldSpawn = 10.0f;
	private float powerSpawn = 20.0f;
	private bool isSpawnShield = true;
	private bool isSpawnPower = true;
	public AudioSource backGroundMusic;
	public AudioSource destroyPlayerSound;
	public AudioSource motherShipSpawn;
	private bool isBossPlaying = false;
	private bool isBGMPlaying = true;
	
	void Start()
	{
		isGameOver = false;
		isRestart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves (enemyShip));
		StartCoroutine (SpawnWaves (movingRock));
		StartCoroutine (SpawnHumanoids (humanoid));
		bossFightDelay = Time.time + bossFightDelay;
		shieldSpawn = Time.time + shieldSpawn;
		powerSpawn = Time.time + powerSpawn;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P)){
			if(mainCamera.GetComponent<CameraController> ().isPaused == true){
				mainCamera.GetComponent<CameraController> ().isPaused = false;
				gameOverText.color = Color.white;
				gameOverText.text = "";
				Time.timeScale = 1;	
			}
			else{
				mainCamera.GetComponent<CameraController> ().isPaused = true;
				gameOverText.color = Color.white;
				gameOverText.text = "Paused";
				Time.timeScale = 0;	
			}
		}

		if (isRestart) {
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if (Time.time > bossFightDelay - 5 && isBGMPlaying == true && isGameOver == false) {
			fadeOut(backGroundMusic);
		}

		if (Time.time > bossFightDelay - 3 && isBossPlaying == false && isGameOver == false) {
			motherShipSpawn.Play ();
			isBossPlaying = true;
		}

		if (Time.time > bossFightDelay && isBossFight == true && isGameOver == false) {
			isBossFight = false;
			SpawnMotherShip(motherShip);
		}

		if (Time.time > shieldSpawn && isGameOver == false && isSpawnShield == true) {
			isSpawnShield = false;
			SpawnShield();
		}

		if (Time.time > powerSpawn && isGameOver == false && isSpawnPower == true) {
			isSpawnPower = false;
			SpawnPower();
		}

		if (isGameOver) {
			DisplayRestart();
		}
	}

	IEnumerator SpawnWaves(Obstacle obstacleObject)
	{
		yield return new WaitForSeconds(obstacleObject.startWait);
		while (!isGameOver && Time.time < (bossFightDelay - 8)) 
		{
			for (int i = 0; i < obstacleObject.obstacleCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (
					obstacleObject.spawnValues.x + mainCamera.transform.position.x, 
					obstacleObject.spawnValues.y, 
					Random.Range (-obstacleObject.spawnValues.z, obstacleObject.spawnValues.z)
					);
				Instantiate (obstacleObject.obstacle, spawnPosition, obstacleObject.obstacle.transform.rotation);
				yield return new WaitForSeconds (obstacleObject.spawnWait);
			}
			
			yield return new WaitForSeconds(obstacleObject.waveWait);
			
			if(isGameOver)
			{
				break;
			}
		}
	}

	IEnumerator SpawnHumanoids(Obstacle obstacleObject)
	{
		yield return new WaitForSeconds(obstacleObject.startWait);
		while (!isGameOver && Time.time < (bossFightDelay - 8)) 
		{
			for (int i = 0; i < obstacleObject.obstacleCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (
					obstacleObject.spawnValues.x + mainCamera.transform.position.x,
					obstacleObject.spawnValues.y,
					obstacleObject.spawnValues.z
					);

				Instantiate (obstacleObject.obstacle, spawnPosition, obstacleObject.obstacle.transform.rotation);
				yield return new WaitForSeconds (Random.Range(1.0f, 3.0f) + obstacleObject.spawnWait);
			}
			
			yield return new WaitForSeconds(obstacleObject.waveWait);
			
			if(isGameOver)
			{
				break;
			}
		}
	}

	void SpawnMotherShip(GameObject motherShip)
	{
		Vector3 spawnPosition = new Vector3 (
			motherShip.transform.position.x + mainCamera.transform.position.x, 
			motherShip.transform.position.y, 
			motherShip.transform.position.z
			);
		Instantiate (motherShip, spawnPosition, motherShip.transform.rotation);
	}
	
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void BossDeath()
	{
		gameOverText.text = "Congrats, you have won!!!";
		isGameOver = true;
	}
	
	public void GameOver()
	{
		destroyPlayerSound.Play ();
		if (motherShipSpawn.isPlaying) {
			motherShipSpawn.Stop();
		}
		gameOverText.color = Color.red;
		gameOverText.text = "Game Over!";
		isGameOver = true;
	}

	public void IncreaseMotherShipHealth()
	{
		bossHealth += 20;
	}

	void SpawnShield(){
		Vector3 spawnPosition = new Vector3 (
			mainCamera.transform.position.x, 
			0.0f, 
			Random.Range(-shield.transform.position.z, shield.transform.position.z)
			);
		Instantiate (shield, spawnPosition, shield.transform.rotation);
	}

	void SpawnPower(){
		Vector3 spawnPosition = new Vector3 (
			mainCamera.transform.position.x, 
			0.0f, 
			Random.Range(-power.transform.position.z, power.transform.position.z)
			);
		Instantiate (power, spawnPosition, power.transform.rotation);
	}

	void fadeOut(AudioSource audioSource){
		audioSource.volume = audioSource.volume - 0.01f;
		if(audioSource.volume < 0){
			isBGMPlaying = false;
			audioSource.Stop();
		}
	}

	void DisplayRestart(){
		restartText.text = "Press 'R' to Restart";
		isRestart = true;
	}
}
