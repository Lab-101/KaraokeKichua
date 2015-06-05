using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {
	public TextAsset creditsFile;
	
	[SerializeField]
	private Text textField;

	void Awake(){
		textField.text = creditsFile.text;
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
