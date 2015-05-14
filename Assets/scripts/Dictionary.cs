using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Dictionary : MonoBehaviour {
	[SerializeField]
	[Multiline]
	private string message;
	[SerializeField]
	private string url;

	[SerializeField]
	private Text textField;
	[SerializeField]
	private Button openUrlButton;
	
	void Awake(){
		textField.text = message;
		openUrlButton.onClick.AddListener(delegate{
			Application.OpenURL(url);
//			Application.OpenURL(WWW.EscapeURL(url));
		});
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
