using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class GameSettings : ScriptableObject {
	public const string ASSET_NAME = "GameSettings";

	public float songStartTime;
	public float songPlayTime;
	public string wordActivityName;
	public string wordActivityInstruction;
	public string writeActivityName;
	public string writeActivityInstruction;
	public List<string> nameLevel;
	public List<string> bestScoreMessage;
	public List<string> normalScoreMessage;
	public List<string> regularScoreMessage;

	private static GameSettings instance; 
	
	public static GameSettings Instance 
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load (ASSET_NAME) as GameSettings;
			}
			
			
			return instance;
		}
	}
}
