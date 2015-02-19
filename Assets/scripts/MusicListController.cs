using UnityEngine;
using System.Collections;

public class MusicListController : MonoBehaviour {
	public MusicListUI musicListUI;
	public SongsList songsList;

	// Use this for initialization
	void Start () {
		musicListUI.drawMusicList(songsList.songs);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
