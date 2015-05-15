using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dictionary : MonoBehaviour {
	[SerializeField]
	[Multiline]
	private string message;
	[SerializeField]
	private string url;

	[SerializeField]
	private Text textField;
	
	void Awake(){
		textField.text = message;
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
