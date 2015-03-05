using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhraseUI : MonoBehaviour {
	public Button letterVisiblePrefab;
	public Button letterHidePrefab;
	private int numberWords;
	
	void Start () {
	}

	public void DrawPhrase(Phrase phrase){
		foreach (Word word in phrase.words){
			foreach(char letter in word.text){
				createLetter(letter+"", word.isHide);
			}
			numberWords++;
			if (numberWords < phrase.words.Count)
				createLetter("", false);
		}
	}

	private Button GetPrefab(bool isWordHide){
		if(isWordHide)
			return Instantiate(letterHidePrefab) as Button;

		return Instantiate(letterVisiblePrefab) as Button;
	}

	private void createLetter(string letter, bool isLetterHide){
		Button newItem;
		newItem = GetPrefab (isLetterHide);
		newItem.image.color = isLetterHide ? Color.cyan : Color.white;
		newItem.name = letter;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = letter;
	}
}
