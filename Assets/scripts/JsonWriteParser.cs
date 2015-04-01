using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class JsonWriteParser {

	private int levelFilter; 
	private WriteActivityData data;
	//private List<Song> songsList;

	public void SetLevelFilter(int level) {
		this.levelFilter = level;
	}

	public WriteActivityData Data {
		get {
			return data;
		}
	}
	
	public string JSONString {
		set {
			data = CreateWriteData (Json.Deserialize (value)as List<object>);
		}
	}

	private WriteActivityData CreateWriteData(List<object> levels)
	{
		WriteActivityData data = new WriteActivityData ();
		foreach(Dictionary<string, object> level in levels){
			if( levelFilter.ToString() == level["level"].ToString() )
				data.phrases = CreatePhrasesList(level["phrases"] as List<object>);
		}
		return data;
	}
		
	private List<Phrase> CreatePhrasesList (List<object> phrases)
	{
		List<Phrase> phrasesList = new List<Phrase> ();
		foreach(Dictionary<string, object> phrase in phrases){
			phrasesList.Add(CreatePhraseObject(phrase));
		}
		return phrasesList;
		
	}
	
	Phrase CreatePhraseObject (Dictionary<string,object> phrase)
	{
		Phrase phraseObject = new Phrase ();
		phraseObject.words = CreateWordsList (phrase);
		return phraseObject;
	}
	
	List<Word> CreateWordsList (Dictionary<string,object> phrase)
	{
		PhraseSplitter splitter = new PhraseSplitter ();
		splitter.FullPhrase = phrase ["phrase"].ToString ();
		splitter.HiddenWords = phrase["words"] as List<object>;
		return splitter.WordsList;
	}

}
