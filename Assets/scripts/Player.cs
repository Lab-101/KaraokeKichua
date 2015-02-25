using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float songStartTime;
	public float songPlayTime;
	private float songLengthInSeconds;

	public AudioSource audioSource;

	public void setAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}

	public void play(float startTime, float playTime ){
		Stop ();
		songLengthInSeconds = playTime;
		audioSource.time = startTime;
		audioSource.Play ();
	}

	public void Stop(){
		audioSource.Stop ();
	}

	void Update () {
		if (songLengthInSeconds > 0) {
			songLengthInSeconds -= Time.deltaTime;
			if (songLengthInSeconds <= 0) {
				play(songStartTime, songPlayTime);
			}
		}
	}

	public float GetSongLength (){
		float songLength = audioSource.clip.length;
		return songLength;
	}


}
