using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class WriteActivity : Activity {
	// Data Activity
	public TextAsset json;
	private WriteActivityData data = new WriteActivityData ();
	private Phrase phrase = new Phrase();

	// GUI Objects
	[SerializeField]
	private PhraseUI phraseUI;
	[SerializeField]
	private WordUI wordUI;
	[SerializeField]
	private Image imageHiddenWord;

	void Awake(){
		ReadDataFromJson ();

		wordUI.LetterButtonSelected += HandleLetterButtonSelected;
		phraseUI.WordFinished += HandleWordFinished;
		phraseUI.PhraseFinished += HandlePhraseFinished;
		ActivityReseted += HandleActivityReseted;	
	}

	private void ReadDataFromJson (){
		JsonWriteParser parser = new JsonWriteParser();
		parser.SetLevelFilter (level);
		parser.JSONString = json.text;
		data = parser.Data;	
	}

	private void GetHiddenWordByIndex (int indexHiddenWord)	{
		Word nextWord = GetHiddenWordByIndex (phrase.words, indexHiddenWord);
		if (nextWord != null) {
			wordUI.DrawWord (nextWord.text);
			imageHiddenWord.sprite = nextWord.image;
		} else {
			SetActivityAsFinished ();
		}
	}
	
	private Phrase GetRandomPhraseFromSong(List<Phrase> phrases){
		if (phrases.Count != 0) {
			int randomIndex = UnityEngine.Random.Range(0, phrases.Count);
			return phrases[randomIndex];		
		}
		throw new Exception ("La cancion no tiene frases");
	}

	private Word GetHiddenWordByIndex (List<Word> words, int hiddenWordNumber){
		int hiddenWordCounter = 0;
		foreach (Word word in words) {
			if (word.isHidden){
				if (hiddenWordNumber == hiddenWordCounter)
					return word;
				
				hiddenWordCounter++;
			}
		}
		return null;
	}
	
	private void DestroyPhrase(){
		phraseUI.ClearHiddenWords ();
		foreach(Transform  child in phraseUI.transform ) {
			Destroy (child.gameObject);
		}
	}
	
	private void DestroyWord(){
		foreach(Transform  child in wordUI.transform ) {
			Destroy (child.gameObject);
		}
	}
	
	private void HandleActivityReseted(){
		ReadDataFromJson ();
		DestroyPhrase ();
		DestroyWord ();
		phrase = GetRandomPhraseFromSong (data.phrases);
		phraseUI.DrawPhrase (phrase);
		GetHiddenWordByIndex (0);
		gameStateBehaviour.GameState = GameState.WriteActivity;
		result.RetryActionExecuted = StartActivity;
	}
	
	private void HandleLetterButtonSelected (Button letterButton) {
		string letter = letterButton.transform.GetChild (0).GetComponent<Text> ().text;
		letterButton.interactable = !phraseUI.CheckCorrectLetter (letter);
	}
	
	private void HandleWordFinished (int indexNextWord)	{
		DestroyWord ();
		GetHiddenWordByIndex (indexNextWord);
	}
	
	private void HandlePhraseFinished () {
		resultsButton.gameObject.SetActive(true);
	}
}
