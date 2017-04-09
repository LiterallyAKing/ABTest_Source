using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public AnalyticsManager analyticsman;

	SaveData save;
	System.DateTime timestart;
	


	public void NewPlayerStart() {
		SaveRef.SaveInitialize();
		//NewPlayerStart();

		save = new SaveData();
		if (analyticsman.experimentmode) {
			save.AddData("GameType", "Experiment");
		} else {
			save.AddData("GameType", "Control");
		}
		save.AddData("DateOfSession", System.DateTime.Now.ToString("MM-dd-yyyy"));
		save.AddData("Play_StartTime", System.DateTime.Now.ToString("H:mm:ss"));
		timestart = System.DateTime.Now;
	}

	public bool NewPlayerEnd() {
		save.AddData("Play_EndTime", System.DateTime.Now.ToString("H:mm:ss"));
		save.AddData("Play_TotalTime", System.DateTime.Now.Subtract(timestart).TotalMilliseconds.ToString());
		save.AddData("Total#OfRuns", analyticsman.NumOfRuns.ToString());
		save.AddData("AvgTimePerRun", analyticsman.AvgTimePerRun.ToString("#,000.000"));
		save.AddData("TotalMoneyEarned", analyticsman.TotalMoneyEarned.ToString());
		save.AddData("TotalMoneySpent", analyticsman.TotalMoneySpent.ToString());
		save.AddData("#SpeedUpgradesPurchased", analyticsman.SpeedUpgradesBought.ToString());
		save.AddData("#ThrusterUpgradesPurchased", analyticsman.ThrusterUpgradesBought.ToString());
		save.AddData("#ThrustsAttemptedTotal", analyticsman.ThrusterAttemptsTotal.ToString());
		save.AddData("#ThrustsInitiatedTotal", analyticsman.ThrusterInitiatedTotal.ToString());
		save.AddData("#BouncersHitTotal", analyticsman.BouncersHitTotal.ToString());
		save.AddData("#BouncersHitDuringThrustTotal", analyticsman.BouncersHitDuringThrust.ToString());
		save.AddData("FurthestDistanceAchieved", analyticsman.HighestDistanceAchieved.ToString());
		save.AddData("AvgDistImprovementPerRun", analyticsman.AvgDistImprovementPerRun.ToString("#,000.000"));

		SaveRef.SaveData(save.GetData());
		return true;
	}
	

}
