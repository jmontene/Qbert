using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	public LevelLayout layout;
	public EnemySpawnData spawnData;
	public QBert qbertPrefab;
	public GameManager gameManager;

	public float victoryWait = 3f;
	public float defeatWait = 2f;

	[HideInInspector]
	public Space[][] spaces;
	[HideInInspector]
	public QBert qbert;
	[HideInInspector]
	public List<QBertCharacter> currentEnemies;

	List<Cube> cubes;
	List<Elevator> elevators;
	int lightedCubes;

	// Use this for initialization
	void Start () {
		lightedCubes = 0;
		cubes = new List<Cube> ();
		elevators = new List<Elevator> ();
		currentEnemies = new List<QBertCharacter> ();

		MakeLevel ();
		InitQBert ();

		InvokeRepeating ("SpawnEnemy", spawnData.spawnRate, spawnData.spawnRate);
	}

	public void OnLightedCube(){
		gameManager.AddScore (25);
		++lightedCubes;
		if (lightedCubes == cubes.Count) {
			Victory ();
		}
	}

	public void MoveCharacterTo(QBertCharacter character, Vector2Int pos, bool teleport = false, bool fall = false){
		Space s = GetSpaceAt (pos);
		if (s == null) {
			Debug.LogWarning ("Null space found in level");
			return;
		}

		if (teleport) {
			s.Teleport (character);
		} else {
			s.Move (character, fall);
		}
	}

	public Space GetSpaceAt(Vector2Int pos){
		if (pos.x < 0 || pos.x >= spaces.GetLength (0) || pos.y < 0 || pos.y >= spaces [0].Length) {
			return null;		
		} else {
			return spaces [pos.x] [pos.y];
		}
	}

	public void AddScore(int score){
		gameManager.AddScore (score);
	}

	public void RemoveEnemy(QBertCharacter e){
		currentEnemies.Remove (e);
		Destroy (e.gameObject);
	}

	public void KillQBert(){
		StartCoroutine (KillQBertCo ());
	}

	IEnumerator KillQBertCo(){
		qbert.SetSpeechBubble (true);
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime (2.5f);
		Time.timeScale = 1f;
		RemoveAllEnemies ();
		qbert.SetSpeechBubble (false);

		gameManager.LoseLife ();
		if (gameManager.lives == 0) {
			Defeat ();
		}
	}

	public void OnQBertFallEnd(){
		RemoveAllEnemies ();
		gameManager.LoseLife ();
		if (gameManager.lives == 0) {
			Defeat ();
		} else {
			MoveCharacterTo (qbert, layout.initialPos, true);
		}
	}

	public void OnGreenBall(){
		StartCoroutine (OnGreenBallCo ());
	}

	IEnumerator OnGreenBallCo(){
		currentEnemies.ForEach ((e) => e.frozen = true);
		CancelInvoke ();
		yield return new WaitForSeconds (2f);
		InvokeRepeating ("SpawnEnemy", spawnData.spawnRate, spawnData.spawnRate);
		currentEnemies.ForEach ((e) => e.frozen = false);
	}

	void InitQBert(){
		qbert = Instantiate (qbertPrefab, transform.position, Quaternion.identity) as QBert;
		qbert.Init (this);
		MoveCharacterTo (qbert, layout.initialPos, true);
	}

	void Victory(){
		CancelInvoke ();
		RemoveAllEnemies ();
		SoundManager.instance.PlaySFX ("level_clear");
		qbert.listenToInput = false;
		gameManager.AddScore (1000);
		foreach (Elevator e in elevators) {
			if (!e.used) {
				gameManager.AddScore (100);
			}
		}
		cubes.ForEach ((c) => c.StartFlash ());
		Invoke ("EndGame", victoryWait);
	}

	void Defeat(){
		CancelInvoke ();
		RemoveAllEnemies ();
		qbert.listenToInput = false;
		gameManager.ShowGameOverWindow ();
		Invoke ("EndGame", defeatWait);
	}

	void EndGame(){
		if (LeaderboardsManager.instance.IsHighScore (gameManager.score)) {
			gameManager.ShowHighScoreWindow ();
		} else {
			LeaderboardsManager.instance.SaveRecords ();
			SceneManager.LoadScene ("menu");
		}
	}

	void RemoveAllEnemies(){
		foreach(QBertCharacter e in currentEnemies){
			Destroy (e.gameObject);
		}
		currentEnemies.Clear ();
	}
		

	void SpawnEnemy(){
		QBertCharacter enemy = spawnData.spawnEnemy (this);
		Vector2Int spawnPoint = layout.spawnPoints [Random.Range (0, layout.spawnPoints.Length)];
		QBertCharacter e = Instantiate (enemy, spaces [spawnPoint.x] [spawnPoint.y].landingPoint.position + Vector3.up * spawnData.spawnOffset, Quaternion.identity) as QBertCharacter;
		e.Init (this);
		currentEnemies.Add (e);

		MoveCharacterTo (e, spawnPoint, false, true);
	}

	void MakeLevel(){
		layout.Init ();
		spawnData.Init ();

		int[][] rows = layout.rows;
		spaces = new Space[rows.GetLength(0)][];

		for (int i = 0; i < rows.Length; ++i) {
			spaces [i] = new Space[rows [i].Length];
			for (int j = 0; j < rows [i].Length; ++j) {
				if (rows [i] [j] >= 0) {
					Vector2 newPos = new Vector2 (transform.position.x + j * layout.offset.x - i * layout.offset.x, transform.position.y + j * layout.offset.y + i * layout.offset.y);
					spaces [i] [j] = Instantiate (layout.availableSpaces [rows [i] [j]], newPos, Quaternion.identity, transform) as Space;
					spaces [i] [j].Init (this, new Vector2Int(i, j));
					if (spaces [i] [j].spaceName == "Cube") {
						cubes.Add ((Cube)spaces [i] [j]);
					} else if (spaces [i] [j].spaceName == "Elevator") {
						elevators.Add ((Elevator)spaces [i] [j]);
					}
				} else {
					spaces [i] [j] = null;
				}
			}
		}
	}
}
