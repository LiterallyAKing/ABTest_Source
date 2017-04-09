using UnityEngine;
using System.Collections;
using System.IO;


public class FileIOUtil {

//	public static void WriteJsonToFile(string fileName, JSONClass json){
//		string output = json.ToString();

//		WriteStringToFile(fileName, output);
//	}

//	public static JSONNode ReadJsonFromFile(string fileName){
////		Debug.Assert(File.Exists(fileName));

//		if(Path.GetExtension(fileName) != ".json"){
//			return null;
//		}

//		JSONNode fileContent = JSONClass.Parse(ReadStringFromFile(fileName));

//		return fileContent;
//	}

//	public static void WriteStringToFile(string fileName, string content){
//		StreamWriter sw = new StreamWriter(fileName, false);

//		sw.WriteLine(content);

//		sw.Close();
//	}

//	/// <summary>
//	/// Reads the string from file.
//	/// </summary>
//	/// <returns>The string from file, if the file does not exist, returns null.</returns>
//	/// <param name="fileName">The name of the file.</param>
//	public static string ReadStringFromFile(string fileName){

//		if(!File.Exists(fileName)){
//			Debug.Log("WARNING: Attempted to read from file \"" + fileName 
//				+ "\", file does not exist!");
//			return null;	
//		}

//		StreamReader sr = new StreamReader(fileName);

//		string output = sr.ReadToEnd();

//		sr.Close();

//		return output;
//	}

}
