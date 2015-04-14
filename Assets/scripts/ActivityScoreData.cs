using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActivityScoreData {

	public int level;
	public string activity;
	public int score;

	public ActivityScoreData () {
		this.level = 0;
		this.activity = "";
		this.score = 0;
	}

}
