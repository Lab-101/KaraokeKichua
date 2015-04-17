using UnityEngine;
using System.Collections;

public class GameStateBehaviour : MonoBehaviour {
	[SerializeField]
	private GameState gameState;

	public GameState GameState {
		get {
			return gameState;
		}
		set {
			gameState = value;
		}
	}
}
