using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {
	[SerializeField]
	private GameStateBehaviour gameState;
	[SerializeField]
	[Multiline]
	private string creditDescription;
	
	[SerializeField]
	private Text textField;

	void Awake(){
		textField.text = creditDescription;
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
