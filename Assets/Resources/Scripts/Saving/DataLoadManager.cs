using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadManager : MonoBehaviour {

	public bool DataFilePresent = false;
	public DebugManager debugman;
	public AnalyticsManager analyticsman;

	// Use this for initialization
	void Start () {
		if (!SaveRef.LoadInitialize()) {
			DataFilePresent = false;
			debugman.DataFileStatus(false);
		} else {
			DataFilePresent = true;
			debugman.DataFileStatus(true);
		}

		//if (DataFilePresent) {
		//	ProcessData(SaveRef.LoadData());
		//}

	}


	public Dictionary<string, string> ExperimentVariables() {
		if (DataFilePresent) {
			return SaveRef.LoadData();
		} else return new Dictionary<string, string>();
	}



}
