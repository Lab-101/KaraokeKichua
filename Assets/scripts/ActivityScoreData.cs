using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActivityScoreData {

	public int level;
	public int activity;
	public int score;

	public ActivityScoreData () {
		this.level = 0;
		this.activity = 0;
		this.score = 0;
	}

}
