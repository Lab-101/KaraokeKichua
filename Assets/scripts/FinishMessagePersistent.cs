using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class FinishMessagePersistent {

	public static FinishMessageData finishMessageData = new FinishMessageData();

	public static void Save(FinishMessageData finishMessageData) {
		SetEnvironment ();
		BinaryFormatter buffer = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + Path.DirectorySeparatorChar + "FinishMessageData.gd"); 		
		FinishMessagePersistent.finishMessageData = finishMessageData;
		buffer.Serialize(file, FinishMessagePersistent.finishMessageData);
		file.Close();
	}	
	
	public static void Load() {
		SetEnvironment ();
		if(File.Exists(Application.persistentDataPath +  Path.DirectorySeparatorChar + "FinishMessageData.gd")) {
			BinaryFormatter buffer = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "FinishMessageData.gd", FileMode.Open);
			FinishMessagePersistent.finishMessageData = (FinishMessageData)buffer.Deserialize(file);
			file.Close();
		}
	}

	public static void ClearFinishMessageData (){		
		FinishMessageData finishMessageData = new FinishMessageData();
		Save(finishMessageData);
	}
	
	public static bool GetFinishedGameMessageVisibleState(){
		Load ();
		return finishMessageData.isFinishedGameMessageVisible;
	}
	
	public static void SaveFinishedGameMessageVisibleState(bool state){
		Load ();
		FinishMessageData finishMessageData = new FinishMessageData();
		finishMessageData.isFinishedGameMessageVisible = state;
		
		Save (finishMessageData);
	}

	private static void SetEnvironment ()	{
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
	}

}