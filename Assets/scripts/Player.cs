using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	public AudioSource audioSource;
	private float songLengthInSeconds;

	public Action PlayFinished {
		get;
		set;
	}
	
	void Update () {
		if (songLengthInSeconds > 0) {
			songLengthInSeconds -= Time.deltaTime;
			if (songLengthInSeconds <= 0) {
				if(PlayFinished != null)
					PlayFinished();
				//Play(songStartTime, songPlayTime);
			}
		}
	}

	private void SetAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}

	private void Play(float startTime, float playTime ){
		Stop ();
		songLengthInSeconds = playTime;
		audioSource.time = startTime;
		audioSource.Play ();
	}

	public void PlaySong(AudioClip song ){
		SetAudioClipToAudioSource (song);
		Play (0, GetSongLength () + 2);
	}

	public void PlayPreview(AudioClip song ){
		SetAudioClipToAudioSource (song);
		Play (GameSettings.Instance.songStartTime, GameSettings.Instance.songPlayTime);
	}

	public void Stop(){
		audioSource.Stop ();
	}

	public float GetSongLength (){
		float songLength = audioSource.clip.length;
		return songLength;
	}

	public void SetSongLengthInSeconds (float seconds){
		 songLengthInSeconds = seconds;
	}

	public void Pause(){		
		if (audioSource.isPlaying)
			audioSource.audio.Pause ();
		else
			audioSource.Play ();
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}