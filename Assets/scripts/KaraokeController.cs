using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {	
	public Button finishButton;
	public Action songPreview;
	
	void Start () {
		finishButton.onClick.AddListener(delegate {
			if(songPreview != null)
				songPreview();
		});
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

}
