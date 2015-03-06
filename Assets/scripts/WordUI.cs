using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordUI : MonoBehaviour {

	public Button letterRandomPrefab;
	
	void Start () {
	}
	
	public void DrawWord(string word){
		string randomWord = RandomizeWord(word);
		
		foreach(char letter in randomWord){
			createLetter(letter+"");
		}
	}
		
	private void createLetter(string letter){
		Button newItem;
		newItem = Instantiate(letterRandomPrefab) as Button;
		newItem.name = letter;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = letter;
	}

	private string RandomizeWord(string word){
		return word;
	}

}
