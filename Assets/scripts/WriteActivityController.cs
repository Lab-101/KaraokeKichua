using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

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
		wordUI.DrawWord (phrase.words [0].text);
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
