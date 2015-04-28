
using NUnit.Framework;
using System.IO;

public class TextFileReaderTest{

	private TextFileReader textFileReader;
	private StreamReader objectReader;

	public TextFileReaderTest(){
		textFileReader = new TextFileReader ();
	}

	[Test]
	public void TestValidObjectTextFileReader(){
		Assert.IsNotNull(textFileReader);
	}

	[Test]
	public void TestValidPathFile(){
		textFileReader.setPathFile ("/Editor/translations/");
		string pathFile = textFileReader.getPathFile ();
		Assert.AreEqual(pathFile,"/Editor/translations/");
	}

	[Test]
	public void TestValidFileName(){
		textFileReader.setFileName("prueba.txt");
		Assert.AreEqual(textFileReader.getFileName(),"prueba.txt");
	}

	/*[Test]
	public void TestValidObjectFile(){
		textFileReader.setFileName("prueba.txt");
		objectReader = textFileReader.getObjectFile ();
		Assert.IsNotNull (objectReader);
	}*/

	[Test]
	public void TestValidInformationFile(){
		textFileReader.setPathFile ("/Editor/translations/");
		textFileReader.setFileName("prueba.txt");
		string str = textFileReader.getFileReader();
		Assert.AreEqual(str,"La lengua quechuaa");
	}
}
