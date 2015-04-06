using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public KaraokeController karaoke;
	public WriteActivity writeActivity;
	public WordActivity wordActivity;
	public ResultsController results;
	public GameStateBehaviour gameStateBehaviour;
	public GameObject mainScreen;

	private bool IsSelectingSong 
	{
		get{
			return gameStateBehaviour.GameState == GameState.SelectingSong;
		}
	}

	private bool IsPlayingSong
	{
		get{
			return gameStateBehaviour.GameState == GameState.PlayingSong;
		}
	}

	private bool IsPlayingWriteActivity {
		get{
			return gameStateBehaviour.GameState == GameState.WriteActivity;
		}
	}

	private bool IsPlayingWordActivity {
		get{
			return gameStateBehaviour.GameState == GameState.WordActivity;
		}
	}

	private bool IsShowingResults {
		get{
			return gameStateBehaviour.GameState == GameState.ShowingResults;
		}
	}

	private bool IsSelectingLevel {
		get{
			return gameStateBehaviour.GameState == GameState.SelectingLevel;
		}
	}

	void Start () {
		gameStateBehaviour.GameState = GameState.SelectingLevel;

		karaoke.SetActive ();
		writeActivity.SetActive ();
		wordActivity.SetActive ();
		results.SetActive ();		

		karaoke.SetInactive ();
		writeActivity.SetInactive ();
		wordActivity.SetInactive ();
		results.SetInactive ();
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 

		if (IsSelectingSong) {
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
			mainScreen.SetActive(false);
		} else if (IsPlayingSong) {
			karaoke.SetActive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
			mainScreen.SetActive(false);
		} else if (IsPlayingWriteActivity) {
			karaoke.SetInactive ();
			writeActivity.SetActive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
			mainScreen.SetActive(false);
		}else if (IsPlayingWordActivity) {
			karaoke.SetInactive ();
			wordActivity.SetActive ();
			writeActivity.SetInactive ();
			results.SetInactive ();
			mainScreen.SetActive(false);
		} else if (IsShowingResults){
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetActive();
			mainScreen.SetActive(false);
		} else if(IsSelectingLevel) {
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetInactive();
			mainScreen.SetActive(true);
		}else {
			karaoke.SetInactive ();
		}
	}

}
