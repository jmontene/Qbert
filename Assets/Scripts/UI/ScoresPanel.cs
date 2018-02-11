using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresPanel : MonoBehaviour {

	public RectTransform scoreRecordPrefab;

	int maxRecords;

	// Use this for initialization
	void Start () {
		maxRecords = LeaderboardsManager.instance.maxAmountOfRecords;
		PopulatePanel ();
	}
	
	void PopulatePanel(){
		List<LeaderboardsManager.LeaderboardRecord> records = LeaderboardsManager.instance.GetSortedRecords ();

		for (int i = 0; i < records.Count; ++i) {
			RectTransform r = Instantiate<RectTransform>(scoreRecordPrefab, transform);
			r.anchorMin = new Vector2 (0f, (1f / (float)maxRecords) * (maxRecords - i - 1));
			r.anchorMax = new Vector2 (1f, (1f / (float)maxRecords) * (maxRecords - i));
			r.offsetMin = Vector2.zero;
			r.offsetMax = Vector2.zero;

			r.transform.Find ("Name").GetComponent<Text> ().text = records [i].name;
			r.transform.Find ("Score").GetComponent<Text> ().text = records [i].score.ToString();
			r.transform.Find ("Position").GetComponent<Text> ().text = (i+1).ToString();
		}
	}
}
