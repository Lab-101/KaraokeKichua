using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class GameSettingsEditor
{


	private static GameSettings ObjInstance
	{
		get
		{
			GameSettings instance = Resources.Load(GameSettings.ASSET_NAME) as GameSettings;
			if (instance == null)
			{
				instance = ScriptableObject.CreateInstance<GameSettings>();
				AssetHelper.CreateAsset (GameSettings.ASSET_NAME, instance);
			}

			return instance;
		}
	}
	
	[MenuItem("Config/Game Settings")]
	public static void Edit()
	{
		Selection.activeObject = ObjInstance;
	}

	/*
	public static void SwitchTo (GameEnvironment environment)
	{
		GameSettings.Instance.CurrentEnvironment = environment;
		EditorUtility.SetDirty(GameSettings.Instance);
	}*/
}
