using UnityEngine;
using System;
using System.Collections;

public class SubtitleLoader {
	public string URL;
	public Action<string> SubtitlesObtained;
	
	public IEnumerator Start() {
		WWW www = new WWW(URL);
		yield return www;
		if (SubtitlesObtained != null)
			SubtitlesObtained(www.text);
	}
}