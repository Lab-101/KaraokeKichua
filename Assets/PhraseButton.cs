using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PhraseButton : MonoBehaviour {

	public Action <Button> ButtonPressed;

	void Update(){
		if (this.GetComponent<BoxCollider2D>().size.x <= 1)
			this.GetComponent<BoxCollider2D>().size = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y);
	}

	void OnMouseDown () {
		if (ButtonPressed != null)
			ButtonPressed(this.GetComponent<Button>());
	}
}
