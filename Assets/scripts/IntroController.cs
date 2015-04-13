using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class IntroController : MonoBehaviour {
	[SerializeField]
	public Button continueButton;
	public Action ContinueButtonClicked;

	void Awake () {
		continueButton.onClick.AddListener (delegate {
			if(ContinueButtonClicked != null)
				ContinueButtonClicked();
		});
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

}
