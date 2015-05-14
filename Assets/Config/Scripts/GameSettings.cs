using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class GameSettings : ScriptableObject {
	public const string ASSET_NAME = "GameSettings";
	
	public bool clearDataOnStartGame;
	public float songStartTime;
	public int scoreByCurrentLevel;
	public float songPlayTime;
	public List<string> wordActivityTag;
	public List<string> writeActivityTag;
	public List<string> phraseActivityTag;
	public List<string> imageActivityTag;
	public List<string> nameLevel;
	public List<string> bestScoreMessage;
	public List<string> normalScoreMessage;
	public List<string> regularScoreMessage;
	public bool isPosibleShowMessage;
	public bool isRegularKichua;

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
