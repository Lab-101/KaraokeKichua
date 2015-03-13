using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class JsonSongsParser {

	private List<Song> songsList; 

	public string JSONString {
		set {
			songsList = CreateSongsList (Json.Deserialize (value)as List<object>);
		}
	}

	public List<Song> SongsList  {
		get {
				return songsList;
		}
	}

	private List<Song> CreateSongsList(List<object> songs)
	{
		List<Song> songsList = new List<Song> ();
		foreach(Dictionary<string, object> song in songs){
			songsList.Add(CreateSongObject(song));
		}
		return songsList;
	}

	private Song CreateSongObject (Dictionary<string, object> song)
	{
		Song songObject = new Song ();
		songObject.urlSong = song ["name"].ToString();
		songObject.phrases = CreatePhrasesList(song["phrases"] as List<object>);
		return songObject;
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

	Word CreateWordObject (Dictionary<string, object> word)
	{
		Word wordObject = new Word ();
		wordObject.text = word ["text"].ToString();
		return wordObject;
	}
}


	
