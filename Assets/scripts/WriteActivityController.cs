using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WriteActivityController : MonoBehaviour {

	public Button exitButton;
	public PhraseUI phraseUI;
	public WordUI wordUI;
	public Button resultsButton;
	private Phrase phrase;

	[SerializeField]
	private Image imageHiddenWord;

	public Action BackActionExecuted {
		get;
		set;
	}

	public Action ActivityFinished {
		get;
		set;
	}

	void Start () {

		exitButton.onClick.AddListener (delegate {
			if(BackActionExecuted != null){
				BackActionExecuted();
			}
		});

		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				resultsButton.gameObject.SetActive(false);
				ActivityFinished();
			}
		});

		wordUI.LetterButtonSelected += HandleLetterButtonSelected;
		phraseUI.WordFinished += HandleWordFinished;
		phraseUI.PhraseFinished += HandlePhraseFinished;
	}

	void HandleLetterButtonSelected (Button letterButton) {
		string letter = letterButton.transform.GetChild (0).GetComponent<Text> ().text;
		letterButton.interactable = !phraseUI.CheckCorrectLetter (letter);
	}

	void HandleWordFinished (int indexNextWord)	{
		DestroyWord ();
		GetHiddenWordByIndex (indexNextWord);
	}

	void HandlePhraseFinished () {
		resultsButton.gameObject.SetActive(true);
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	public void Reset(Song song){
		SetActive ();
		DestroyPhrase ();
		DestroyWord ();
		phrase = GetRandomPhraseFromSong (song);
		phraseUI.DrawPhrase (phrase);
		GetHiddenWordByIndex (0);
	}

	void GetHiddenWordByIndex (int indexHiddenWord)	{
		Word nextWord = GetHiddenWordByIndex (phrase.words, indexHiddenWord);
		if (nextWord != null) {
			wordUI.DrawWord (nextWord.text);
			imageHiddenWord.sprite = nextWord.image;
		}



	}

	private Phrase GetRandomPhraseFromSong(Song song){
		if (song.phrases.Count != 0) {
			int randomIndex = UnityEngine.Random.Range(0, song.phrases.Count);
			return song.phrases[randomIndex];		
		}
		throw new Exception ("La cancion no tiene frases");
	}

	private void SetPhrase(Phrase phrase){
		this.phrase = phrase;
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
}