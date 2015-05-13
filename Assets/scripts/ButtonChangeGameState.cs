using UnityEngine;
using System.Collections;

public class ButtonChangeGameState : MonoBehaviour{

	public GameStateBehaviour gameState;
	public GameState targetState;

	public void ChangeGameState(){
		gameState.GameState = targetState;
	}

}
