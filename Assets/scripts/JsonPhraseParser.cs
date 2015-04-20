using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Text.RegularExpressions; 

public class JsonPhraseParser {

	private int levelFilter; 
	private PhraseActivityData data;

	public void SetLevelFilter(int level) {
		this.levelFilter = level;
	}

	public PhraseActivityData Data {
		get {
			return data;
		}
	}
	
	public string JSONString {
		set {
			data = CreateWriteData (Json.Deserialize (value)as List<object>);
		}
	}

	private PhraseActivityData CreateWriteData(List<object> levels)
	{
		PhraseActivityData data = new PhraseActivityData ();
		foreach(Dictionary<string, object> level in levels){
			if( levelFilter.ToString() == level["level"].ToString() ){
				data.phrases = CreatePhrasesList(level["phrases"] as List<object>);
				return data;
			}
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
		phraseObject.phraseTranslated = phrase ["phraseTranslated"].ToString ();
		phraseObject.words = SplitBySpaces (phrase ["phrase"].ToString());
		return phraseObject;
	}
	
	List<Word> CreateWordsList (Dictionary<string,object> phrase)
	{
		PhraseSplitter splitter = new PhraseSplitter ();
		splitter.FullPhrase = phrase ["phrase"].ToString ();
		return splitter.WordsList;
	}

	private List<Word> SplitBySpaces(string phrase){
		List<Word> phraseSplitedList = new List<Word>();
		string [] phraseSplited = phrase.Split(' ');
		for (int i = 0; i < phraseSplited.Length; i++){
			Word word = new Word();
			word.text = phraseSplited[i];
			word.isHidden = true;
			phraseSplitedList.Add(word);
		}
		return phraseSplitedList;
	}
}