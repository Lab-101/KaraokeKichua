using UnityEngine;
using System.Collections;

public class WordActivity : Activity {

	private WordActivityData data;
	public TextAsset json;

	void Start(){
		data = new WordActivityData ();
		JsonWordsParser parser = new JsonWordsParser ();
		parser.SetLevelFilter (1);
		parser.JSONString = json.text;
		data = parser.Data;
	}

}
