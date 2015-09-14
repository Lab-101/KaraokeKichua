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
	
	public void DrawButtonsByWord(List<Word> wordList){
		List<Word> randomList = GetNewListOfWords (wordList);
		phraseText = GetPhraseByListOfWords (wordList);
		randomList.Shuffle ();
		if (wordList.Count > 1)
			while (phraseText == GetPhraseByListOfWords (randomList))
				randomList.Shuffle ();
		foreach(Word word in randomList){
			randomWordsButtons.Add(createWordButton(word.text));
		}
	}

	private List<Word> GetNewListOfWords (List<Word> list){
		List<Word> newList = new List<Word> (); 
		foreach(Word word in list){
			newList.Add(new Word(word));
		}
		return newList;
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
		if (RandomWordOfPhraseSelected!=null) {
			PhraseButton phraseButton = newItem.GetComponent<PhraseButton>() as PhraseButton;
			phraseButton.ButtonPressed = RandomWordOfPhraseSelected;
		}
		return newItem;
	}
	
	public void DestroyAllButtons(){
		foreach(Button button in randomWordsButtons) {
			Destroy (button.gameObject);
		}
		randomWordsButtons.Clear ();
	}

}
