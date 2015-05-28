using UnityEngine;
using System.Collections;

public class WordAudioPlayer : MonoBehaviour {
	public AudioSource audioSource;
	private AudioClip wordAudioClip;

	public void SetWordToPlay (string word){
		if (word != null) {
			wordAudioClip = Resources.Load ("word_records/palabra_" + word.ToLower(), typeof(AudioClip)) as AudioClip;
			SetAudioClipToAudioSource (wordAudioClip);
		}
	}
	
	private void SetAudioClipToAudioSource(AudioClip clip){
		audioSource.clip = clip;
	}

	public void PlayWord(){
		Play ();
	}

	public void Stop (){
		audioSource.Stop ();
	}
	
	public void Pause (){
		audioSource.Pause ();
	}
	
	public void Resume (){
		audioSource.Play ();
	}

	private void Play(){
		Stop ();
		Resume ();
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

}
