using UnityEngine;
using System.Collections;

public class ImageActivity : Activity {
	
	void Awake(){
		ActivityStarted += HandleActivityStarted;	
		ActivityDataReseted += ReadDataFromJson;	
	}
	
	private void ReadDataFromJson (){
		CheckIsDataFound ();
	}
	
	private void CheckIsDataFound() {
		isDataFound = true;
	}
	
	private void HandleActivityStarted () {
		BeginActivity ();
	}
	
	private void BeginActivity ()	{
		gameStateBehaviour.GameState = GameState.ImageActivity;
	}
}

