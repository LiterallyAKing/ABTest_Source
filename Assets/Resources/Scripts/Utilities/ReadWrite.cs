using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public static class ReadWrite {


	public static string DataPath(){
		string toreturn;
		toreturn = Application.persistentDataPath;
		#if UNITY_EDITOR
		toreturn = Application.dataPath;
		#endif
		return toreturn;
	}


	//const string ALLOWED_CHARACTERS = @"[\W]";//@"^[a-zA-Z0-9\s,]*$";
	const string DISALLOWED_CHARACTERS = "[^0-9A-Za-z ]+$";//@"\W";//@"[a-zA-Z0-9\s,]*$";

	public static bool IsAlphaNumeric(string strToCheck)
	{
		return !Regex.IsMatch(strToCheck, DISALLOWED_CHARACTERS);
	}

	public static bool ContainsWhiteSpace(string strToCheck) 
	{
		return Regex.IsMatch(strToCheck, @"\s+");
	}

	public static string ReplaceNonAlphaNumeric(this string str)
	{
		return Regex.Replace(str, DISALLOWED_CHARACTERS, string.Empty);
	}


	public static void WriteToFile(string fileName, string content, bool append = true, char delim = '|'){
		fileName = DataPath() + "/" + fileName;
		StreamWriter sw = new StreamWriter(fileName, append);
		sw.WriteLine (content);
		Debug.Log (fileName);
		sw.Close ();
	}
	public static void WriteToFile(string fileName, string[] content, bool append = true, char delim = '|'){
		fileName = DataPath() + "/" + fileName;
		StreamWriter sw = new StreamWriter(fileName, append);
		string output = "";
		for (int i = 0; i < content.Length; i++) {
			output += (content [i] + delim.ToString ());
		}
		sw.WriteLine (output);
		sw.Close ();
	}

	/// <summary>
	/// Reads the string from file.
	/// </summary>
	/// <returns>The string from file, if the file does not exist, returns null.</returns>
	/// <param name="fileName">The name of the file.</param>
	public static string ReadStringFromFile(string fileName){

		if(!File.Exists(fileName)){
			Debug.Log("WARNING: Attempted to read from file \"" + fileName 
				+ "\", file does not exist!");
			return null;	
		}

		StreamReader sr = new StreamReader(fileName);

		string output = sr.ReadToEnd();

		sr.Close();

		return output;
	}



}
