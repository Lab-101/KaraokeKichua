using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RandomWordsList : MonoBehaviour {

	public Button wordPrefab;
	private List<Button> randomWordsButtons = new List<Button>();
	public Action <Button> RandomWordSelected;

	public Action SelectedCorrectWords {
		get;
		set;
	}

	public void DrawButtonsByWord(List<string> wordList){
		wordList.Shuffle ();
		foreach(string word in wordList){
			randomWordsButtons.Add(createWordButton(word));
		}
	}

	private Button createWordButton(string name){
		Button newItem;
		newItem = Instantiate(wordPrefab) as Button;
		newItem.name = name;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = name;
		newItem.onClick.AddListener (delegate {
			if(RandomWordSelected!=null)
				RandomWordSelected(newItem);
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