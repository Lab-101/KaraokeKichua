using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LevelDataPersistent {
	
	public static List<LevelData> dataLevels = new List<LevelData>();
	
	public static void Save(List<LevelData> data) {
		BinaryFormatter buffer = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/LevelData.gd"); 
		LevelDataPersistent.dataLevels = data;
		buffer.Serialize(file, LevelDataPersistent.dataLevels);
		file.Close();
	}	
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/LevelData.gd")) {
			BinaryFormatter buffer = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/LevelData.gd", FileMode.Open);
			LevelDataPersistent.dataLevels = (List<LevelData>)buffer.Deserialize(file);
			file.Close();
		}
	}
	
	public static void ClearDataLevels (){		
		List<LevelData> levelData = new List<LevelData>();
		Save(levelData);
	}
	
	public static bool IsLevelUnlock(int level){
		foreach (LevelData levelData in dataLevels) {			
			if(levelData.level == level)
				return levelData.isUnlocked;			
		}
		
		return false;
	}
	
	public static void SaveLevelData(LevelData data){
		Load ();

		foreach (LevelData levelData in dataLevels) {						
			if(levelData.level == data.level){
				levelData.isUnlocked = data.isUnlocked;
				Save(dataLevels);
				return; 
			}			
		}
		
		dataLevels.Add (data);
		Save (dataLevels);
	}

}
