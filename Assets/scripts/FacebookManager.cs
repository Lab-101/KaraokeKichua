using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	public Button loginButton;

	// Use this for initialization
	void Start () {

		loginButton.onClick.AddListener (delegate {
			HandleLoginSelected();
		});
	}
	
	void Update () {
	
	}
	
	private void HandleLoginSelected ()
	{
		Debug.Log ("Se presiono el boton de login");
	}
}
