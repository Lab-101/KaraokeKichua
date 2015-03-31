using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class JsonWordsParser  {

	private int levelFilter; 
	private WordActivityData data;
	
	public void SetLevelFilter(int level) {
		this.levelFilter = level;
	}

	public WordActivityData Data {
		get {
			return data;
		}
	}
		
	public string JSONString {
		set {
			data = CreateWordsDataList (Json.Deserialize (value)as List<object>);
		}
	}
	
	private WordActivityData CreateWordsDataList(List<object> levels) {
		WordActivityData data = new WordActivityData ();
		foreach(Dictionary<string, object> level in levels){
			if( levelFilter.ToString() == level["level"].ToString() )
				data = CreateWordActivityDataObject(level);
		}
		return data;
	}
	
	private WordActivityData CreateWordActivityDataObject (Dictionary<string, object> levelData)	{
		WordActivityData data = new WordActivityData ();
		data.songName = levelData ["name"].ToString();
		data.wordsList = CreateWordsList( levelData ["showWords"] as List<object>);
		data.wordsValidsList = CreateWordsList( levelData["wordsValids"] as List<object>);
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
