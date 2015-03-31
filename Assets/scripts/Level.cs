using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int numberLevel;
	private Activity writeActivity;
	private WordActivity wordActivity;

	void Start(){
		wordActivity = new WordActivity ();
	}

}
