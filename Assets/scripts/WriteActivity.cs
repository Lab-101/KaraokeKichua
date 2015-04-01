using UnityEngine;
using System.Collections;

public class WriteActivity : MonoBehaviour {

	protected Score score;
	public TextAsset json;
	private WriteActivityData data;

	void Start(){
		data = new WriteActivityData ();
		JsonWriteParser parser = new JsonWriteParser();
		parser.SetLevelFilter (1);
		parser.JSONString = json.text;
		data = parser.Data;		
	}
}
