using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ActivityScorePersistent {

	public static List<ActivityScoreData> scoresActivities = new List<ActivityScoreData>();
			
	public static void Save(List<ActivityScoreData> dataActivitiesScore) {
		BinaryFormatter buffer = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/ActivityScore.gd"); 		
		ActivityScorePersistent.scoresActivities = dataActivitiesScore;
		buffer.Serialize(file, ActivityScorePersistent.scoresActivities);
		file.Close();
	}	
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/ActivityScore.gd")) {
			BinaryFormatter buffer = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/ActivityScore.gd", FileMode.Open);
			ActivityScorePersistent.scoresActivities = (List<ActivityScoreData>)buffer.Deserialize(file);
			file.Close();
		}
	}

	public static void ClearScoresActivities (){		
		List<ActivityScoreData> scoresActivities = new List<ActivityScoreData>();
		Save(scoresActivities);
	}
	
	public static int GetScoreByActivityAndLevel(int level, int activity){
		foreach (ActivityScoreData scores in scoresActivities) {			
			if(level == scores.level && activity == scores.activity)
				return scores.score;			
		}
		
		return 0;
	}
	
	public static void SaveActivityLevelScore(ActivityScoreData data){
		Load ();

		foreach (ActivityScoreData scoreActivity in scoresActivities) {			
			if(data.level == scoreActivity.level && data.activity == scoreActivity.activity){
				scoreActivity.score = data.score;
				Save(scoresActivities);
				return; 
			}			
		}
				
		scoresActivities.Add (data);
		Save (scoresActivities);
	}

}
