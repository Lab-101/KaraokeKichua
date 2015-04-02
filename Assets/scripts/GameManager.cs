using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public MusicListController musicList;
	public KaraokeController karaoke;
	public WriteActivity writeActivity;
	public WordActivity wordActivity;
	public ResultsController results;
	public GameState gameState;

	private bool IsSelectingSong 
	{
		get{
			return gameState == GameState.SelectingSong;
		}
	}

	private bool IsPlayingSong
	{
		get{
			return gameState == GameState.PlayingSong;
		}
	}

	private bool IsPlayingWriteActivity {
		get{
			return gameState == GameState.WriteActivitySong;
		}
	}

	private bool IsPlayingWordActivity {
		get{
			return gameState == GameState.WordActivitySong;
		}
	}

	private bool IsShowingResults {
		get{
			return gameState == GameState.ShowingResults;
		}
	}
	
	void Start () {
		gameState = GameState.SelectingSong;
		musicList.SongStarted += HandleSongStarted;
		musicList.SongFinished += HandleSongFinished;
		karaoke.SongFinished += HandleFinishSongButtonSelected;
		karaoke.SongPaused += HandleSongPaused;
		wordActivity.ActivityFinished += HandleActivityFinished;
		results.BackActionExecuted += HandleBackActionExecuted;
		results.RetryActionExecuted += HandleRetryActionExecuted;
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
		gameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (musicList.subtitleList, musicList.GetAudioSourceFromPlayer());
	}
	
	private void HandleSongFinished (){

	if (IsPlayingSong) {
		StartWordActivity();
	} else if(IsSelectingSong){			
		musicList.RestartPlayer ();
	}
}
	private void HandleFinishSongButtonSelected (){
		if (IsPlayingSong) {
			StartWordActivity ();
		}
	}

	private void StartWriteActivity ()
	{
		gameState = GameState.WriteActivitySong;
		musicList.SetInactive ();
		writeActivity.Reset ();
	}

	private void StartWordActivity ()
	{
		gameState = GameState.WordActivitySong;
		musicList.SetInactive ();
		wordActivity.Reset (musicList.selectedSong);
	}
	
	private void HandleSongPaused (){
		musicList.PauseSong ();
	}
	
	private void HandleBackActionExecuted (){
		gameState = GameState.SelectingSong;
		musicList.RestartPlayer ();
	}

	private void HandleActivityFinished() {
		gameState = GameState.ShowingResults;
	}
	private void HandleRetryActionExecuted() {
		gameState = GameState.WriteActivitySong;
		StartWriteActivity ();
	}
}
