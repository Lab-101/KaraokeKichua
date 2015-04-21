using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RandomWordUI : MonoBehaviour {

	public Button wordPrefab;
	private List<Button> randomWordsButtons = new List<Button>();
	public Action <Button> RandomWordOfPhraseSelected;
	public string phraseText = "";
	public Action SelectedCorrectWords {
		get;
		set;
	}
	
	public void DrawButtonsByWord(List<Word> wordList){
		phraseText = GetPhraseByListOfWords (wordList);
		wordList.Shuffle ();
		if (wordList.Count > 1)
			while (phraseText == GetPhraseByListOfWords (wordList))
				wordList.Shuffle ();
		foreach(Word word in wordList){
			randomWordsButtons.Add(createWordButton(word.text));
		}
	}

	private string GetPhraseByListOfWords (List<Word> list){
		string newPhrase = "";
		foreach(Word word in list)
			newPhrase += word.text + " ";
		return newPhrase;
	}

	private Button createWordButton(string name){
		Button newItem;
		newItem = Instantiate(wordPrefab) as Button;
		newItem.name = name;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = name;
		newItem.onClick.AddListener (delegate {
			if(RandomWordOfPhraseSelected!=null)
				RandomWordOfPhraseSelected(newItem);
		});
		return newItem;
	}
	
	public void DisableAllButtons(){
		foreach(Button word in randomWordsButtons){
			if(word != null )word.interactable = false;
		}
		FinishGame ();
	}
	
	private void FinishGame(){
		if (SelectedCorrectWords!=null)
			SelectedCorrectWords();
	}
}
