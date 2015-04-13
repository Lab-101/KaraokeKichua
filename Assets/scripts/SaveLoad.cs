using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static List<ActivityScore> dataActivitiesScore = new List<ActivityScore>();
			
	public static void Save(List<ActivityScore> dataActivitiesScore) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/ActivityScoreGame.gd"); 
		bf.Serialize(file, SaveLoad.dataActivitiesScore);
		file.Close();
	}	
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/ActivityScoreGame.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/ActivityScoreGame.gd", FileMode.Open);
			SaveLoad.dataActivitiesScore = (List<ActivityScore>)bf.Deserialize(file);
			file.Close();
		}
	}

	public static void ClearDataScores (){		
		List<ActivityScore> dataActivitiesScore = new List<ActivityScore>();
		Save(dataActivitiesScore);
	}
	
	public static int FindActivityLevelScore(int level, int activity){

		foreach (ActivityScore scores in dataActivitiesScore) {
			
			if(level == scores.level && activity == scores.activity)
				return scores.score;
			
		}
		
		return 0;
	}
	
	public static void SaveActivityLevelScore(int level, int activity, int score){

		Load ();
		
		foreach (ActivityScore scores in dataActivitiesScore) {
			
			if(level == scores.level && activity == scores.activity){
				scores.score = score;
				Save(dataActivitiesScore);
				return; 
			}
			
		}
		
		ActivityScore data = new ActivityScore ();
		data.level = level;
		data.activity = activity;
		data.score = score;
		
		dataActivitiesScore.Add (data);
		Save (dataActivitiesScore);
	}

	public static bool IsLevelUnLock(int level, int numberActivitiesPreviousLevel) {
		Load ();
		
		foreach (ActivityScore scores in dataActivitiesScore) {
			if (level == scores.level)
				return true; 
		}

		if (level > 1) {
			int totalActivitiesPreviousLevel = GetCountActivityByLevel (level - 1);

			if(numberActivitiesPreviousLevel == totalActivitiesPreviousLevel)
				return true;
			else
				return false;
		}


		return true;
	}

	public static int GetCountActivityByLevel (int level){

		int totalActivities = 0;

		foreach (ActivityScore scores in dataActivitiesScore) {

			if(level == scores.level){
				totalActivities++;
			}

		}

		return totalActivities;
	}
}
