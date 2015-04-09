using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class IntroController : MonoBehaviour {

	[SerializeField]
	public Button introButton;
	[SerializeField]
	public Button returnButton;
	[SerializeField]
	private GameObject introScreen;
	// Use this for initialization
	void Start () {
		introButton.onClick.AddListener(delegate {
			introScreen.SetActive(true);
		} );
		returnButton.onClick.AddListener(delegate {
			introScreen.SetActive(false);
		} );

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
