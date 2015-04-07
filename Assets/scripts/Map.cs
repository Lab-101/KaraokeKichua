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
		IdentifyActiveLevel (levelOneButton);

		levelOneButton.onClick.AddListener(delegate {
			IdentifyCurrentLevel (levelOneButton);
		} );

		levelTwoButton.onClick.AddListener(delegate {
			IdentifyCurrentLevel (levelTwoButton);
		} );
	}

	private void IdentifyCurrentLevel (Button levelButton)	{
		levelButton.image.color = Color.green;
	}

	private void IdentifyActiveLevel (Button levelButton)
	{
		levelButton.enabled = true;
		levelButton.image.color = Color.red;
	}

}
