using UnityEngine;
using System.Collections;
using System.IO;

public class TextFileReader : MonoBehaviour {

	private string fileName;
	private string pathFile;
	
	public string getFileName() {
		return this.fileName;
	}
			
	public void setFileName (string fileName){
		this.fileName = fileName;
	}

	public void setPathFile (string pathFile)
	{
		this.pathFile = pathFile;
	}

	public string getPathFile ()
	{
		return this.pathFile;
	}

	public string getFileReader ()
	{
		string line="";
		line = File.ReadAllText(Application.dataPath+""+getPathFile()+""+getFileName());
		return line;
	}
}
