using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WriteActivityController : MonoBehaviour {
	public Button exitButton;
	public Action songPreview;
	public PhraseUI phraseUI;
	public WordUI wordUI;
	private Phrase phrase;

	void Start () {
		exitButton.onClick.AddListener(delegate {
			if(songPreview != null){
				songPreview();
			}
		});
		wordUI.LetterButtonSelected += HandleLetterButtonSelected;
		phraseUI.FinishedWord += HandleFinishedWord;
	}

	void HandleLetterButtonSelected (Button letterButton)	{
		string letter = letterButton.transform.GetChild (0).GetComponent<Text> ().text;
		letterButton.interactable = !phraseUI.CheckCorrectLetter (letter);
	}

	void HandleFinishedWord (int indexNextWord)	{
		DestroyWord ();
		wordUI.DrawWord (GetNextWord(phrase.words, indexNextWord));
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	public void Reset(Song song){
		DestroyPhrase ();
		DestroyWord ();
		phrase = GetRandomPhraseFromSong (song);
		phraseUI.DrawPhrase (phrase);
		wordUI.DrawWord (GetNextWord(phrase.words, 0));
	}

	private Phrase GetRandomPhraseFromSong(Song song){
		if (song.pharses.Count != 0) {
			int randomIndex = UnityEngine.Random.Range(0, song.pharses.Count);
			return song.pharses[randomIndex];		
		}
		throw new Exception ("La cancion no tiene frases");
	}

	private void SetPhrase(Phrase phrase){
		this.phrase = phrase;
	}

	private string GetNextWord (List<Word> words, int hiddenWordNumber){
		int hiddenWordCounter = 0;
		foreach (Word word in words) {
			if (word.isHide){
				if (hiddenWordNumber == hiddenWordCounter)
					return word.text;

				hiddenWordCounter++;
			}
				
		}
		throw new Exception ("No hay palabras ocultas");
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
