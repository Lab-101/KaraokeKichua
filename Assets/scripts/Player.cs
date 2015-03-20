using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	public AudioSource audioSource;
	private float songLengthInSeconds;
	private float timeAudioSource;

	public Action PlayFinished {
		get;
		set;
	}
	
	void Update () {
		if (songLengthInSeconds > 0) {
			songLengthInSeconds -= Time.deltaTime;

			if (songLengthInSeconds <= 0 ) {
				if(PlayFinished != null)
					audioSource.Stop();
					PlayFinished();					
			}
		}
	}

	public void PlaySong(AudioClip song ){
		SetAudioClipToAudioSource (song);
		Play (0, GetSongLength () + 2);
	}

	public void PlayPreview(AudioClip song ){
		SetAudioClipToAudioSource (song);
		Play (GameSettings.Instance.songStartTime, GameSettings.Instance.songPlayTime);
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	public void SetSongLengthInSeconds (float seconds){
		songLengthInSeconds = seconds;
	}


	public bool IsPlaying ()	{
		return audioSource.isPlaying;
	}

	public void Pause (){
		timeAudioSource = audioSource.time;
		audioSource.Pause ();
	}

	public void Resume (){
		audioSource.time = timeAudioSource;
		audioSource.Play ();
	}
	
	private float GetSongLength (){
		float songLength = audioSource.clip.length;
		return songLength;
	}

	private void SetAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}
	
	private void Play(float startTime, float playTime ){
		audioSource.Stop ();
		songLengthInSeconds = playTime;
		audioSource.time = startTime;
		audioSource.Play ();
	}
}