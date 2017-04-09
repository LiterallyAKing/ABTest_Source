using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData {

	Dictionary<string, string> dataDict = new Dictionary<string, string>();


	public SaveData() {
		for (int i = 0; i < SaveRef.COL_headers.Length; i++) {
			dataDict.Add(SaveRef.COL_headers[i], "#NULL!");
		}
		dataDict["PlayerID"] = "Player_" + System.DateTime.Now.ToString("MMddyyhhmmssff");
	}


	public void AddData(string column, string data) {
		dataDict[column] = data;
	}

	public string GetData() {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < SaveRef.COL_headers.Length; i++) {
			if (i < SaveRef.COL_headers.Length - 1) {
				sb.Append(dataDict[SaveRef.COL_headers[i]] + SaveRef.COL_DELIM);
			} else {
				sb.Append(dataDict[SaveRef.COL_headers[i]]);
			}
		}
		return sb.ToString();
	}
			

}
