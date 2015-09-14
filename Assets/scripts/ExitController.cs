using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitController : MonoBehaviour {
	public Text exitMessage;
	public Button okButton;
	public Button cancelButton;
	public GameObject exitPanel;

	public Player player;
	public GameState currentGameState ;
	public GameStateBehaviour gameStateBehaviour;

	private bool IsSelectingLevel {
		get{
			return currentGameState == GameState.SelectingLevel;
		}
	}


	void Start () {
		cancelButton.onClick.RemoveAllListeners ();
		cancelButton.onClick.AddListener (() => CancelExit ());
	}

	public void ChangeCurrentGameState (GameState state){
		currentGameState = state;
		UpdateExitMessage ();
		UpdateOnClickOkButton ();
	}

	private void UpdateExitMessage(){		
		if (IsSelectingLevel) {
			exitMessage.text = "¿Quieres salir de la aplicacion?";
		} else {
			exitMessage.text = "¿Quieres volver al menu princial?";
		}
	}

	private void UpdateOnClickOkButton(){
		okButton.onClick.RemoveAllListeners ();
		if (IsSelectingLevel) {
			okButton.onClick.AddListener(() => ExitApplication());
		} else {
			okButton.onClick.AddListener(() => BackToMainPanel());
		}
	}

	private void CancelExit(){
		exitPanel.SetActive (false);
	}

	private void ExitApplication(){
		Application.Quit (); 
	}

	private void BackToMainPanel(){
		gameStateBehaviour.GameState = GameState.SelectingLevel;
		player.Stop ();
		exitPanel.SetActive (false);
	}
}
