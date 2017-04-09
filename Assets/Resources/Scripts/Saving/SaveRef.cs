using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

[System.Serializable]
public static class SaveRef {
	public static float controlpercent = 0;
	public static char COL_DELIM = ',';
	public static string SaveDirectory = Application.dataPath + "/Resources/RecordedData/";
	public static string SaveFileName = "PlayerData";
	public static string LoadFileName = "GameVariables";
	public static string SaveExtension = ".csv";
	public static string LoadFileExtension = ".txt";
	public static string DATA_fieldmarker = "=";
	public static string DATA_commentmarker = "//";
	public static string[] COL_headers = new string[] {"PlayerID",
														"GameType",
														"DateOfSession",
														"Play_StartTime",
														"Play_EndTime",
														"Play_TotalTime",
														"Total#OfRuns",
														"AvgTimePerRun",
														"TotalMoneyEarned",
														"TotalMoneySpent",
														"#SpeedUpgradesPurchased",
														"#ThrusterUpgradesPurchased",
														"#ThrustsAttemptedTotal",
														"#ThrustsInitiatedTotal",
														"#BouncersHitTotal",
														"#BouncersHitDuringThrustTotal",
														"FurthestDistanceAchieved",
														"AvgDistImprovementPerRun"
														};
	public static string[] DATA_fields = new string[] {"PlayerGravity",
														"CannonRotateSpeed",
														"CannonMaxAngle",
														"CannonMinAngle",
														"InitialLaunchVelocity",
														"ThrusterDuration",
														"ThrusterCooldownTime",
														"ThrusterForce_X",
														"ThrusterForce_Y",
														"GroundSpeedReduction",
														"BouncerSpeedReduction",
														"BouncerForce_X",
														"BouncerForce_Y",
														"BouncersBaseFrequency",
														"BouncersChanceDecreasePerHundredFt",
														"DistanceToEarnOneCurrency",
														"SpeedBoostInitialCost",
														"SpeedBoostCostIncreaseExponent",
														"ThrustersInitialCost",
														"ThrustersCostIncreaseExponent"
														};


	//This runs to check if the directory exists and if the file exists, and if not creates them. Adds column headers to data file.
	public static void SaveInitialize() {
		DirectoryInfo dir = System.IO.Directory.CreateDirectory(SaveDirectory);
		if (!File.Exists(SaveDirectory + SaveFileName + SaveExtension)) {
			StreamWriter sw = new StreamWriter(SaveDirectory + SaveFileName + SaveExtension, true);
			string cols = "";
			for (int i = 0; i < COL_headers.Length; i++) {
				if (i < COL_headers.Length - 1) {
					cols += COL_headers[i] + COL_DELIM;
				} else {
					cols += COL_headers[i];
				}
			}
			sw.WriteLine(cols);
			sw.Close();
		}
	}
	
	public static void SaveData(string content) {
		StreamWriter sw = new StreamWriter(SaveDirectory + SaveFileName + SaveExtension, true);
		sw.WriteLine(content);
		//sw.WriteLine("test" + System.DateTime.Now.ToString("hh:mm:ss.fff"));
		sw.Close();
	}

	public static bool LoadInitialize() {
		if (!File.Exists(SaveDirectory + LoadFileName + LoadFileExtension)) {
			return false;
		}
		return true;
	}

	public static void UpdateControlPercent() {
		//Get control/experiment amt
		StreamReader sr = new StreamReader(SaveDirectory + SaveFileName + SaveExtension);
		string line;
		int experiment = 0;
		int control = 0;

		using (sr) {
			do {
				line = sr.ReadLine();
				if (line != null) {
					if (line.Contains("Control")) {
						control++;
					} else if (line.Contains("Experiment")) {
						experiment++;
					}
				}
			} while (line != null);
			sr.Close();
		}
		if ((experiment + control) == 0) {
			controlpercent = 0f;
		} else {
			controlpercent = (float)control / (float)(experiment + control);
		}
	}

	public static Dictionary<string, string> LoadData() {
		Dictionary<string, string> toreturn = new Dictionary<string, string>();

		List<string> test = new List<string>();

		StreamReader sr = new StreamReader(SaveDirectory + LoadFileName + LoadFileExtension);

		string line;

		using (sr) {
			// While there's lines left in the text file, do this:
			do {
				line = sr.ReadLine();
				if (line != null) {
					line = line.Replace(" ", string.Empty);
					test.Add(line);

					int equalsindex = line.IndexOf(DATA_fieldmarker);
					if (equalsindex > 0) {
						string field = line.Substring(0, equalsindex);
						if (DATA_fields.Any(field.Contains)) {
							int commentindex = line.IndexOf(DATA_commentmarker);
							if (commentindex > 0) {
								toreturn.Add(field, line.Substring(equalsindex+1,commentindex - equalsindex -1));
							} else {
								toreturn.Add(field, line.Substring(equalsindex+1));
							}
						}
					}


				}
			} while (line != null); 
			sr.Close();
		}
		//string[] lines = toreturn.Select(kvp => kvp.Key + ">>>" + kvp.Value.ToString()).ToArray<string>();
		//Debug.Log(string.Join(System.Environment.NewLine, lines));
		//for (int i = 0; i < test.Count; i++) {
		//	Debug.Log(test[i]);
		//}

		return toreturn;
	}

}
