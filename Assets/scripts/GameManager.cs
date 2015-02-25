using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject viewCurrent;
	public MusicListController musicListController;
	public MusicListController musicList;
	public GameObject playerUI;

	public GameState gameState;

	void Start () {
		gameState = GameState.SelectingSong;
		viewCurrent.SetActive (true);
		musicList.songStarted += HandleSongStarted;
	}

	void Update(){
		if (gameState == GameState.SelectingSong) {
			musicList.SetActive();
			playerUI.SetActive(false);
		} else {
			musicList.gameObject.SetActive(false);
			playerUI.SetActive(true);
		}
	}

	private void HandleSongStarted ()
	{
		gameState = GameState.PlayingSong;
	}

	public void changeViewPanel(GameObject view){
		HideView (viewCurrent);
		ShowView (view);
		viewCurrent = view;
	}

	public void ShowView(GameObject view){
		/*musicListController.stopSong();
		float songLengthInSeconds = musicListController.player.GetSongLength ()+2;

		if (view.name == "SongSelectionMenuUI") {
			musicListController.playSongInList (0);
		} else {			
			musicListController.playCurrentSong ();
			//Invoke ("endSongClosesKaraokePanel", songLengthInSeconds);
		}

		view.SetActive (true);*/
	}

	public void HideView(GameObject view){
		view.SetActive (false);
	}

	public void endSongClosesKaraokePanel (){
		Debug.Log("Se acabo");
		musicListController.player.Stop();
		changeViewPanel (viewCurrent);
	}

}
