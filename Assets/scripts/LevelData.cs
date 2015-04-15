using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelData {

	public int level;
	public bool isUnlocked;
	public bool isIntroOpened;
	
	public LevelData () {
		this.level = 0;
		this.isUnlocked = false;
		this.isIntroOpened = false;
	}
}
