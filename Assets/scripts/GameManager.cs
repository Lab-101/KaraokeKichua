using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public KaraokeController karaoke;
	public WriteActivity writeActivity;
	public WordActivity wordActivity;
	public ImageActivity imageActivity;
	public PhraseActivity pharseActivity;
	public ResultsController results;
	public GameStateBehaviour gameStateBehaviour;
	public GameObject mainScreen;
	public IntroController intro;
	public Credits credits;

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

	private bool IsPlayingImageActivity {
		get{
			return gameStateBehaviour.GameState == GameState.ImageActivity;
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

	private bool IsShowingIntro {
		get{
			return gameStateBehaviour.GameState == GameState.ShowingIntro;
		}
	}

	private bool IsPlayingPharseActivity {
		get{
			return gameStateBehaviour.GameState == GameState.PharseActivity;
		}
	}

	private bool IsShowingCredits {
		get{
			return gameStateBehaviour.GameState == GameState.ShowingCredits;
		}
	}

	void Start () {
		gameStateBehaviour.GameState = GameState.SelectingLevel;

		karaoke.SetActive ();
		writeActivity.SetActive ();
		wordActivity.SetActive ();
		imageActivity.SetActive ();
		pharseActivity.SetActive ();
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
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		} else if (IsPlayingSong) {
			karaoke.SetActive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		} else if (IsPlayingWriteActivity) {
			karaoke.SetInactive ();
			writeActivity.SetActive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		}else if (IsPlayingWordActivity) {
			karaoke.SetInactive ();
			wordActivity.SetActive ();
			writeActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		}else if (IsPlayingImageActivity) {
			karaoke.SetInactive ();
			wordActivity.SetInactive ();
			writeActivity.SetInactive ();
			imageActivity.SetActive();
			pharseActivity.SetInactive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		} else if (IsPlayingPharseActivity) {
			karaoke.SetInactive ();
			wordActivity.SetInactive ();
			writeActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetActive();
			results.SetInactive ();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		}else if (IsShowingResults){
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetActive();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetInactive();
		} else if(IsSelectingLevel) {
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive();
			mainScreen.SetActive(true);
			intro.SetInactive();
			credits.SetInactive();
		} else if(IsShowingIntro) {
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive();
			mainScreen.SetActive(true);
			intro.SetActive();
			credits.SetInactive();
		} else if(IsShowingCredits) {
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			imageActivity.SetInactive();
			pharseActivity.SetInactive();
			results.SetInactive();
			mainScreen.SetActive(false);
			intro.SetInactive();
			credits.SetActive();
		} else {
			karaoke.SetInactive ();
		}
	}

	void Awake(){
		Input.multiTouchEnabled = false;
	}
}
