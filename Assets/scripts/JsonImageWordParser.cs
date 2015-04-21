using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class JsonImageWordParser : MonoBehaviour {

	private int levelFilter; 
	private ImageWordActivityData data;
	
	public void SetLevelFilter(int level) {
		this.levelFilter = level;
	}
	
	public ImageWordActivityData Data {
		get {
			return data;
		}
	}
	
	public string JSONString {
		set {
			data = CreateWordsDataList (Json.Deserialize (value)as List<object>);
		}
	}
	
	private ImageWordActivityData CreateWordsDataList(List<object> levels) {
		ImageWordActivityData data = new ImageWordActivityData ();
		foreach(Dictionary<string, object> level in levels){
			if( levelFilter.ToString() == level["level"].ToString() )
				data = CreateWordActivityDataObject(level);
		}
		return data;
	}
	
	private ImageWordActivityData CreateWordActivityDataObject (Dictionary<string, object> levelData)	{
		ImageWordActivityData data = new ImageWordActivityData ();
		data.wordsList = CreateWordsList( levelData ["showWords"] as List<object>);
		data.wordValid = levelData["wordValid"].ToString();
		data.wordTranslated = levelData["wordTranslated"].ToString();
		return data;
	}
	
	private List<string> CreateWordsList (List<object> words)	{
		List<string> wordsList = new List<string> ();
		foreach(string word in words){
			wordsList.Add(word.ToString());
		}
		return wordsList;
		
	}
}
