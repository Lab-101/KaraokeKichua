using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class WordUI : MonoBehaviour {

	public Button letterRandomPrefab;
	public Action <Button> LetterButtonSelected;

	private List<Button> randomLetters = new List<Button>();
	
	void Start () {
		//randomLetters = new List<Button>();
	}


	public void DrawWord(string word){
		string randomWord = RandomizeWord(word);
		foreach(char letter in randomWord){
			randomLetters.Add(createLetter(letter+""));
		}
	}
		
	private Button createLetter(string letter){
		Button newItem;
		newItem = Instantiate(letterRandomPrefab) as Button;
		newItem.name = letter;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = letter;
		newItem.onClick.AddListener (delegate {
			if(LetterButtonSelected!=null)
				LetterButtonSelected(newItem);
		});
		return newItem;
	}

	private List<string> ParseStringToList (string text){
		List<string> letterList = new List<string> ();
		foreach (char letter in text.ToCharArray())
			letterList.Add (letter.ToString());

		return letterList;
	}


	private string RandomizeWord(string word){
		List<string> letterList = ParseStringToList(word);
		letterList.Shuffle ();
		if (letterList.Count > 1)
			while (word == string.Concat(letterList.ToArray()))
				letterList.Shuffle ();

		return string.Concat(letterList.ToArray());
	}

	public void deleteLetter(Button letterButton){
		letterButton.image.color= Color.red;
		letterButton.interactable = false;
	}
}