using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

	[SerializeField]
	private Button levelOneButton;
	[SerializeField]
	private Button levelTwoButton;

	void Start()
	{

		IdentifyInactiveLevels ();

		levelOneButton.onClick.AddListener(delegate {
			IdentifyCurrentLevel (levelOneButton);
		} );

		levelTwoButton.onClick.AddListener(delegate {
			IdentifyCurrentLevel (levelTwoButton);
		} );
	}

	private void IdentifyCurrentLevel (Button levelButton)	{
		IdentifyInactiveLevels ();
		levelButton.image.color = Color.green;
	}

	private void IdentifyInactiveLevels ()
	{
		levelOneButton.image.color = Color.grey;
		levelTwoButton.image.color = Color.grey;
	}

}
