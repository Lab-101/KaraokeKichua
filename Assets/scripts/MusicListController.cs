using UnityEngine;
using System.Collections;

public class MusicListController : MonoBehaviour {
	public SongsList songsList;
	public Player player;

	void Start () {
		playSongInList (0);
	}

	void Update () {
	}

	public void playSongInList(int indexSong){		
		player.setAudioClipToAudioSource (songsList.songs[indexSong].audioSong);
		player.play();
	}
}
