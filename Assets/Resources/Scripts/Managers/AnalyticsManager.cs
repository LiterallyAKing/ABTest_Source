using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour {

	public bool tracking = true;
	public bool experimentmode = false;
	public SaveManager saveman;
	public GameManager gameman;
	public DataLoadManager dataload;
	public SceneLoader sceneload;

	System.DateTime runstarttime;
	System.DateTime runendtime;


	public void NewGameBegin() {
		//pick whether A or B, randomly or decided;
		if (tracking) {
			SaveRef.UpdateControlPercent();
			if (SaveRef.controlpercent < 0.25f) {
				experimentmode = false;
			} else {
				experimentmode = true;
			}
			NewPlayerBeginTracking();
		}
		
	}
	public void NewGameEnd() {
		saveman.NewPlayerEnd();



	}



	void NewPlayerBeginTracking() {
		saveman.NewPlayerStart();
	}


	public void RunStart() {
		runstarttime = System.DateTime.Now;
		NumOfRuns++;
	}
	public void RunEnd() {
		runendtime = System.DateTime.Now;
		TotalTimeInRuns += runendtime.Subtract(runstarttime).Seconds;
	}


	private int _NumOfRuns = 0;
	public int NumOfRuns { get { return _NumOfRuns; } set { _NumOfRuns = value; } }

	private float _TotalTimeInRuns = 0;
	public float TotalTimeInRuns { get { return _TotalTimeInRuns; } set { _TotalTimeInRuns = value; } }
	
	public float AvgTimePerRun { get { return TotalTimeInRuns /(float)NumOfRuns; } }

	private int _TotalMoneyEarned = 0;
	public int TotalMoneyEarned { get { return _TotalMoneyEarned; } set { _TotalMoneyEarned = value; } }

	private int _TotalMoneySpent = 0;
	public int TotalMoneySpent { get { return _TotalMoneySpent; } set { _TotalMoneySpent = value; } }

	private int _SpeedUpgradesBought = 0;
	public int SpeedUpgradesBought { get { return _SpeedUpgradesBought; } set { _SpeedUpgradesBought = value; } }

	private int _ThrusterUpgradesBought = 0;
	public int ThrusterUpgradesBought { get { return _ThrusterUpgradesBought; } set { _ThrusterUpgradesBought = value; } }

	private int _thrusterattemptstotal = 0;
	public int ThrusterAttemptsTotal { get { return _thrusterattemptstotal; } set { _thrusterattemptstotal = value; } }

	private int _thrusterinitiatedtotal = 0;
	public int ThrusterInitiatedTotal { get { return _thrusterinitiatedtotal; } set { _thrusterinitiatedtotal = value; } }

	private int _bouncershittotal = 0;
	public int BouncersHitTotal { get { return _bouncershittotal; } set { _bouncershittotal = value; } }

	private int _bouncershitduringthrust = 0;
	public int BouncersHitDuringThrust { get { return _bouncershitduringthrust; } set { _bouncershitduringthrust = value; } }


	private int _HighestDistanceAchieved = 0;
	public int HighestDistanceAchieved { get { return _HighestDistanceAchieved; } set {if (value > _HighestDistanceAchieved) _HighestDistanceAchieved = value; } }

	public List<float> DistancePerRun = new List<float>();

	public float AvgDistImprovementPerRun { get { return CalcAvgDistImprovement(); }  }


	//save.AddData("Total#OfRuns", "XXX");
	//save.AddData("AvgTimePerRun", "XXX");
	//save.AddData("TotalMoneyEarned", "XXX");
	//save.AddData("TotalMoneySpent", "XXX");
	//save.AddData("#SpeedUpgradesPurchased", "XXX");
	//save.AddData("#ThrusterUpgradesPurchased", "XXX");
	//save.AddData("FurthestDistanceAchieved", "XXX");
	//save.AddData("AvgDistImprovementPerRun", "XXX");



	float CalcAvgDistImprovement() {
		if (DistancePerRun.Count < 2) return 0;

		List<float> improvements = new List<float>();
		for (int i = 1; i < DistancePerRun.Count; i++) {
			improvements.Add(DistancePerRun[i] - DistancePerRun[i - 1]);
		}
		float toreturn = 0;
		for (int i = 0; i < improvements.Count; i++) {
			toreturn += improvements[i];
		}
		toreturn = toreturn / (float)DistancePerRun.Count;
		return toreturn;
	}

}
