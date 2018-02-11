using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsManager : MonoBehaviour {

	public class LeaderboardRecord{
		public string name;
		public int score;

		public LeaderboardRecord(string n, int s){
			name = n;
			score = s;
		}
	}

	private static LeaderboardsManager _instance;

	public static LeaderboardsManager instance{
		get{
			if (_instance == null) {
				_instance = FindObjectOfType<LeaderboardsManager> ();
				if (_instance == null) {
					GameObject obj = new GameObject ();
					_instance = obj.gameObject.AddComponent<LeaderboardsManager> ();
					DontDestroyOnLoad (_instance);
				}
			} else {
				LeaderboardsManager ob = FindObjectOfType<LeaderboardsManager> ();
				if (ob != null && ob != _instance) {
					Destroy (ob);
				}
			}
			return _instance;
		}
	}

	[HideInInspector]
	public List<LeaderboardRecord> records;
	public int maxAmountOfRecords = 5;
	public bool addDummyData = false;

	// Use this for initialization
	void Awake() {
		Screen.SetResolution (1000, 900, false);

		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this);
		}
		records = new List<LeaderboardRecord> ();

		if (addDummyData) {
			AddRecord ("AAA", 40);
			AddRecord ("BBB", 10);
			AddRecord ("CCC", 100);
		}
	}

	public List<LeaderboardRecord> GetSortedRecords(){
		records.Sort ((x, y) => -(x.score.CompareTo (y.score)));
		return records;
	}

	public void AddRecord(string name, int score){
		if (records.Count == maxAmountOfRecords) {
			records.Sort ((x, y) => -(x.score.CompareTo (y.score)));
			records [records.Count - 1] = new LeaderboardRecord (name, score);
		} else {
			records.Add (new LeaderboardRecord (name, score));
		}
	}
}
