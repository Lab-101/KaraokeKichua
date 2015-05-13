using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {
	[SerializeField]
	private Button backButton;
	[SerializeField]
	private GameStateBehaviour gameState;

	void Awake(){
		backButton.onClick.AddListener (delegate {
			gameState.GameState = GameState.SelectingLevel;
		});
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
