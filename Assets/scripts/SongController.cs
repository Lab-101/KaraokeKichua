using System;
using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour {
	public Song selectedSong;
	
	private AudioClip selectedClip;
	private float timeSelectedClip;
	private Song song;
	
	[SerializeField]
	private Player player;
	[SerializeField]
	private KaraokeController karaoke;
	[SerializeField]
	protected GameStateBehaviour gameStateBehaviour;
	
	public Action SongStarted {
		get;
		set;
	}
	
	public Action SongFinished {
		get;
		set;
	}
}