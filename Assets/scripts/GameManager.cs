using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public MusicListController musicList;
	public KaraokeController karaoke;
	public WriteActivity writeActivity;
	public WordActivity wordActivity;
	public ResultsController results;
	public GameStateBehaviour gameStateBehaviour;

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

	void Start () {
		gameStateBehaviour.GameState = GameState.SelectingSong;
		musicList.SongStarted += HandleSongStarted;
		musicList.SongFinished += HandleSongFinished;
		karaoke.SongFinished += HandleFinishSongButtonSelected;
		karaoke.SongPaused += HandleSongPaused;
		results.BackActionExecuted += HandleBackActionExecuted;
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (IsSelectingSong) {
			musicList.SetActive ();
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
		} else if (IsPlayingSong) {
			musicList.SetInactive ();
			karaoke.SetActive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
		} else if (IsPlayingWriteActivity) {
			musicList.SetInactive ();
			karaoke.SetInactive ();
			writeActivity.SetActive ();
			wordActivity.SetInactive ();
			results.SetInactive ();
		}else if (IsPlayingWordActivity) {
			musicList.SetInactive ();
			karaoke.SetInactive ();
			wordActivity.SetActive ();
			writeActivity.SetInactive ();
			results.SetInactive ();
		} else if (IsShowingResults){
			musicList.SetInactive ();
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			wordActivity.SetInactive ();
			results.SetActive();
		} else {
			karaoke.SetInactive ();
		}
	}
	
	private void HandleSongStarted (){
		gameStateBehaviour.GameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (musicList.subtitleList, musicList.GetAudioSourceFromPlayer());
	}
	
	private void HandleSongFinished (){
		if (IsPlayingSong) 
			StartWordActivity();
		else if(IsSelectingSong)			
			musicList.RestartPlayer ();
	}

	private void HandleFinishSongButtonSelected (){
		if (IsPlayingSong) {
			StartWordActivity ();
		}
	}

	private void StartWriteActivity ()	{
		gameStateBehaviour.GameState = GameState.WriteActivity;
		musicList.SetInactive ();
		writeActivity.Reset ();
	}

	private void StartWordActivity ()	{
		gameStateBehaviour.GameState = GameState.WordActivity;
		musicList.SetInactive ();
		wordActivity.Reset ();
	}
	
	private void HandleSongPaused (){
		musicList.PauseSong ();
	}

	private void HandleBackActionExecuted (){
		gameStateBehaviour.GameState = GameState.SelectingSong;
		musicList.RestartPlayer ();
	}

}
