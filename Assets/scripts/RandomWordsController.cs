using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RandomWordsController : MonoBehaviour {

	public Button wordPrefab;
	private List<Button> randomWordsButtons = new List<Button>();
	private List<string> randomWords = new List<string>();

	void Start(){
		randomWords.Add ("uno");
		randomWords.Add ("dos");
		randomWords.Add ("tres");
		randomWords.Add ("cuatro");
		randomWords.Add ("cinco");
		randomWords.Shuffle ();
		DrawButtonsByWord(randomWords);
	}

	public void DrawButtonsByWord(List<string> wordList){
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
		return newItem;
	}
}