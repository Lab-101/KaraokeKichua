using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WriteActivityController : MonoBehaviour {
	public Phrase phrase;
	public Button exitButton;
	public Action songPreview;
	public PhraseUI phraseUI;
	public WordUI wordUI;

	void Start () {
		exitButton.onClick.AddListener(delegate {
			if(songPreview != null){
				songPreview();
			}
		});
		phraseUI.DrawPhrase (phrase);
		wordUI.DrawWord (GetNextWord(phrase.words));
		wordUI.LetterButtonSelected += HandleLetterButtonSelected;
	}

	public string GetNextWord (List<Word> words){
		foreach (Word word in words) {
			if (word.isHide)
				return word.text;
		}
		throw new Exception ("No hay palabras ocultas");
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	void HandleLetterButtonSelected (Button letterButton)	{
		string letter = letterButton.transform.GetChild (0).GetComponent<Text> ().text;
		letterButton.interactable = !phraseUI.CheckCorrectLetter (letter);
	}
}
