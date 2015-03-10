using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class JsonSongs : MonoBehaviour {

	public TextAsset jsonString;

	Dictionary <string,object> dict = new Dictionary <string,object>();

	// Use this for initialization
	void Start () {

	/*	List<object> songList = Json.Deserialize (jsonString.text)as List<object>; //as List<Dictionary<string, object>>;
		foreach(Dictionary<string, object> song in songList){
			Debug.Log("nombre: "+song["name"]);
			foreach(Dictionary<string, object> phrase in song["phrases"] as List<object>)
			{
				Debug.Log("\tfrase: "+phrase["phrase"]);
				foreach(Dictionary<string, object> word in phrase["words"] as List<object>)
				{
					Debug.Log("\t\tpalabra: "+word["word"]);
					Debug.Log("\t\tvisibilidad: "+word["status"]);
				}
			}
		}*/

		List<Song> songsList = CreateSongsList (Json.Deserialize (jsonString.text)as List<object>);
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
		songObject.pharses = CreatePhrasesList(song["phrases"] as List<object>);
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
		phraseObject.words = CreateWordsList(phrase["words"] as List<object>);
		return phraseObject;
	}

	List<Word> CreateWordsList (List<object> words)
	{
		List<Word> wordsList = new List<Word> ();
		foreach(Dictionary<string, object> word in words){
			wordsList.Add(CreateWordObject(word));
		}
		return wordsList;
	}

	Word CreateWordObject (Dictionary<string, object> word)
	{
		Word wordObject = new Word ();
		wordObject.text = word ["text"].ToString();
		wordObject.isHide = (bool)word ["isHidden"] ;
		return wordObject;
	}

	// Update is called once per frame
	void Update () {
	
	}
}


	
