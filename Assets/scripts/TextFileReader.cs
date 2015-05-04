using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class TextFileReader  {

	private string fileName;
	private string pathFile;
	
	public string getFileName() {
		return this.fileName;
	}
			
	public void setFileName (string fileName){
		this.fileName = fileName;
	}

	public void setPathFile (string pathFile)	{
		this.pathFile = pathFile;
	}

	public string getPathFile ()	{
		return this.pathFile;
	}

	public string getFileReader ()	{
		string text = "";
		string pathFile = GetDirectionBySystemOperative ();
		try	{
			StreamReader theReader = new StreamReader(pathFile);
			text = theReader.ReadToEnd();  
			theReader.Close();
		} catch (Exception e) {
			Console.WriteLine("{0}\n", e.Message);
			Debug.Log(pathFile);Debug.Log( e.Message);
			text = "Traduccion no encontrada";
		}
	
		return text;
	}

	private string GetDirectionBySystemOperative (){
		if (Application.platform == RuntimePlatform.Android) 
			return Application.streamingAssetsPath + "/" + pathFile + "/" + fileName + ".txt";
		else if (Application.platform == RuntimePlatform.IPhonePlayer) 
			return "file://" + Application.streamingAssetsPath + "/" + pathFile + "/" + WWW.EscapeURL (fileName).Replace ("+", "%20") + ".txt";
			else 
				return "file://" + Application.streamingAssetsPath + "/" + pathFile + "/" + fileName + ".txt";
		}
	}
