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
	private Activity wordActivity;
	[SerializeField]
	private bool introScreenOpened;
	[SerializeField]
	private static readonly object lockFirstActivityOnlyOnFirstTime = new object();

	// Use this for initialization
	void Awake () {
		introScreenOpened = false;
		ActiveIntroScreen ();
		returnButton.onClick.AddListener (delegate {
			OpenIntroScreenFirstTime();
			DeactiveIntroScreen ();
		});
	}
	
	private void ActiveIntroScreen () {
		introButton.onClick.AddListener (delegate {
			introScreen.SetActive (true);
		});
	}
	
	private void DeactiveIntroScreen ()	{
		returnButton.onClick.AddListener (delegate {
			introScreen.SetActive (false);
		});
	}

	private void OpenIntroScreenFirstTime() {
		lock (lockFirstActivityOnlyOnFirstTime) {
			if (!introScreenOpened) { 
					wordActivity.StartActivity ();
					introScreen.SetActive (false);
					introScreenOpened = true;
				}
			}
		}

	/*private void SetReturnButton (bool introScreenItsOpened) {
		if (!introScreenItsOpened){
			OpenIntroScreenFirstTime();
			}
		else{
			DeactiveIntroScreen();
			}
	}*/

	// Update is called once per frame
	void Update () {
	
	}
}
