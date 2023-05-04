using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class JsonFileManager {
	public static string LoadJsonAsResource(string path){
		string jsonFilePath = path.Replace(".json", "");
		TextAsset loadedJsonfile = Resources.Load<TextAsset>(jsonFilePath);
		return loadedJsonfile.text;
	}

	public static string LoadJsonAsExternalResource(string path){
		path = Application.persistentDataPath + "/" + path;
		if(!System.IO.File.Exists(path)){
			return null;
		}
		string response = "";
		using (var file = new StreamReader(path))
		{
			while(!file.EndOfStream){
				response += file.ReadLine();
			}
			file.Close();
		} // file is automatically closed after reaching the end of the using block
		return response;
	}

	public static void WriteJsonToExternalResource(string path, string content){
		path = Application.persistentDataPath + "/" + path;
		//Debug.Log(path);
		using (var file = File.Create(path))
		{
			byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
			file.Write(contentBytes, 0, contentBytes.Length);
			file.Close();
		} // file is automatically closed after reaching the end of the using block
	}
}
