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
	public Dictionary dictionary;

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

	private bool IsShowingDictionary {
		get{
			return gameStateBehaviour.GameState == GameState.ShowingDictionary;
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

		if (GameSettings.Instance.clearDataOnStartGame) {
				LevelDataPersistent.ClearDataLevels ();
				FinishMessagePersistent.ClearFinishMessageData ();
				ActivityScorePersistent.ClearScoresActivities ();
		}
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 

		if (IsSelectingSong) {
			InactiveAllViews();
		} else if (IsPlayingSong) {
			InactiveAllViews();
			karaoke.SetActive ();
		} else if (IsPlayingWriteActivity) {
			InactiveAllViews();
			writeActivity.SetActive ();
		}else if (IsPlayingWordActivity) {
			InactiveAllViews();
			wordActivity.SetActive ();
		}else if (IsPlayingImageActivity) {
			InactiveAllViews();
			imageActivity.SetActive();
		} else if (IsPlayingPharseActivity) {
			InactiveAllViews();
			pharseActivity.SetActive();
		}else if (IsShowingResults){
			InactiveAllViews();
			results.SetActive();
		} else if(IsSelectingLevel) {
			InactiveAllViews();
			mainScreen.SetActive(true);
		} else if(IsShowingIntro) {
			InactiveAllViews();
			mainScreen.SetActive(true);
			intro.SetActive();
		} else if(IsShowingCredits) {
			InactiveAllViews();
			credits.SetActive();
		} else if(IsShowingDictionary) {
			InactiveAllViews();
			mainScreen.SetActive(true);
			dictionary.SetActive();
		} else {
			karaoke.SetInactive ();
		}
	}

	void Awake(){
		Input.multiTouchEnabled = false;
	}

	private void InactiveAllViews(){
		karaoke.SetInactive ();
		writeActivity.SetInactive ();
		wordActivity.SetInactive ();
		imageActivity.SetInactive();
		pharseActivity.SetInactive();
		results.SetInactive();
		mainScreen.SetActive(false);
		intro.SetInactive();
		credits.SetInactive();
		dictionary.SetInactive();		
	}
}
