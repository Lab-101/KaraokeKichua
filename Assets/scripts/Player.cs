using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float songStartTime;
	public float songPlayTime;
	private float songLengthInSeconds;

	public AudioSource audioSource;

	
	void Update () {
		if (songLengthInSeconds > 0) {
			songLengthInSeconds -= Time.deltaTime;
			if (songLengthInSeconds <= 0) {
				Play(songStartTime, songPlayTime);
			}
		}
	}

	public void SetAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}

	public void Play(float startTime, float playTime ){
		Stop ();
		songLengthInSeconds = playTime;
		audioSource.time = startTime;
		audioSource.Play ();
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