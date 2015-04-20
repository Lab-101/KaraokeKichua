using UnityEngine;
using System.Collections;

public class PhraseActivity : Activity {

	public TextAsset json;
	private PhraseActivityData data = new PhraseActivityData ();
	private Phrase phrase = new Phrase();

	void Awake(){
		if (level != null) {
			ReadDataFromJson ();
		}
		ActivityStarted += HandleActivityStarted;	
		ActivityDataReseted += ReadDataFromJson;	
	}

	private void ReadDataFromJson (){
		JsonPhraseParser parser = new JsonPhraseParser();
		data = new PhraseActivityData();
		parser.SetLevelFilter (level);
		parser.JSONString = json.text;
		data = parser.Data;
		CheckIsDataFound ();
	}

	private void CheckIsDataFound() {
		if (data.phrases.Count == 0) {
			isDataFound = false;
			isCompleted = true;
		} else {
			isDataFound = true;
		}
	}

	private void HandleActivityStarted () {
		BeginActivity ();
	}

	private void BeginActivity ()	{
		gameStateBehaviour.GameState = GameState.PharseActivity;
	}
}
