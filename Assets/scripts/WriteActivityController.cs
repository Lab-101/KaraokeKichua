using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WriteActivityController : MonoBehaviour {
	public Button exitButton;
	public Action songPreview;

	void Start () {
		exitButton.onClick.AddListener(delegate {
			if(songPreview != null){
				songPreview();
			}
		});
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
