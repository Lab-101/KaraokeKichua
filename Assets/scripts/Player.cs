using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float songStartTime;
	public float songPlayTime;
	public static float secondsDurationGame;

	public AudioSource audioSource;

	void Start () {
	}

	public void setAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}

	public void play(){
		audioSource.Stop ();
		secondsDurationGame = songPlayTime;
		audioSource.time = songStartTime;
		audioSource.Play ();
	}

	void Update () {
		if (secondsDurationGame > 0) {
			secondsDurationGame -= Time.deltaTime;
			if (secondsDurationGame <= 0) {
				play ();
			}
		}
	}

}
