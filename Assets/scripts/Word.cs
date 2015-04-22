using UnityEngine;
using System;


[Serializable]
public class Word {
	public string text;
	public Sprite image;
	public Boolean isHidden;

	public Word ()	{
	}

	public Word (Word word)	{
		this.text = word.text;
		this.image = word.image;
		this.isHidden = word.isHidden;
	}
}