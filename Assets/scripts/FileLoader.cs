using UnityEngine;
using System;
using System.Collections;

public class FileLoader {
	public string URL;
	public Action<string> FileObtained;
	
	public IEnumerator Start() {
		WWW www = new WWW(URL);
		yield return www;
		if (FileObtained != null)
			FileObtained(www.text);
	}

}