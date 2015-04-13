using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActivityScore {

	public int level;
	public int activity;
	public int score;

	public ActivityScore () {
		this.level = 0;
		this.activity = 0;
		this.score = 0;
	}

}
