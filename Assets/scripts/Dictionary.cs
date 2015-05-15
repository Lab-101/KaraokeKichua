using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Dictionary : MonoBehaviour {
	[SerializeField]
	[Multiline]
	private string message;
	[SerializeField]
	private string httpUrl;
	
	[SerializeField]
	private Text textField;
	[SerializeField]
	private Button openUrlButton;
	
	void Awake(){
		textField.text = message;
		openUrlButton.onClick.AddListener (delegate {
			Application.OpenURL(httpUrl);
		});
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
