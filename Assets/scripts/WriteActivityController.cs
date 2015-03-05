using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WriteActivityController : MonoBehaviour {
	public Button exitButton;
	public Action songPreview;
	public PhraseUI phraseUI;
	public Phrase phrase;

	void Start () {
		exitButton.onClick.AddListener(delegate {
			if(songPreview != null){
				songPreview();
			}
		});
		phraseUI.DrawPhrase (phrase);
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
