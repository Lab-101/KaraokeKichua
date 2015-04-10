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
	[SerializeField]
	private bool introScreenItsOpened;
	[SerializeField]
	private static readonly object syncLock = new object();
	[SerializeField]
	private Activity wordActivity;

	// Use this for initialization
	void Awake () {
		introScreenItsOpened = false;
		ActiveIntroScreen ();
		returnButton.onClick.AddListener (delegate {
			OpenIntroScreenFirstTime();
			DeactiveIntroScreen ();
		});
	}

	private void OpenIntroScreenFirstTime(){
		lock (syncLock) {
			if (!introScreenItsOpened) { 
					wordActivity.StartActivity ();
					introScreen.SetActive (false);
					introScreenItsOpened = true;
			}
		}
	}

	void DeactiveIntroScreen ()
	{
		returnButton.onClick.AddListener (delegate {
			introScreen.SetActive (false);
		});
	}

	void ActiveIntroScreen ()
	{
		introButton.onClick.AddListener (delegate {
			introScreen.SetActive (true);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
