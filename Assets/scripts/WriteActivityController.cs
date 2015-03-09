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
	}

	void HandleLetterButtonSelected (Button letterButton)	{
		string letter = letterButton.transform.GetChild (0).GetComponent<Text> ().text;
		letterButton.interactable = !phraseUI.CheckCorrectLetter (letter);
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
		wordUI.DrawWord (GetNextWord(phrase.words));
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

	private string GetNextWord (List<Word> words){
		foreach (Word word in words) {
			if (word.isHide)
				return word.text;
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
