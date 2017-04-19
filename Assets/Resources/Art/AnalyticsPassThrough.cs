using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsPassThrough : MonoBehaviour {

	AnalyticsManager analyticsman;
	public bool active = false;
	Dictionary<string, string> dataload = new Dictionary<string, string>();

	public Launcher launcher;
	public InGameManager ingameman;
	public PlayerMoveController player;
	public BouncerMaker bouncers;
	public ShopManager shop;

	void Awake () {
		//if (Application.isEditor) {
		//	active = false;
		//}
		if (active) {
			analyticsman = GameObject.Find("AnalyticsManager").GetComponent<AnalyticsManager>();
			if (analyticsman.experimentmode) {
				dataload = analyticsman.dataload.ExperimentVariables();

				if (dataload.ContainsKey("PlayerGravity")) {
					player.GetComponent<Rigidbody2D>().gravityScale = float.Parse(dataload["PlayerGravity"]);
				}
				if (dataload.ContainsKey("CannonRotateSpeed")) {
					launcher.rotateSpeed = float.Parse(dataload["CannonRotateSpeed"]);
				}
				if (dataload.ContainsKey("CannonMaxAngle")) {
					launcher.angleMax = float.Parse(dataload["CannonMaxAngle"]);
				}
				if (dataload.ContainsKey("CannonMinAngle")) {
					launcher.angleMin = float.Parse(dataload["CannonMinAngle"]);
				}
				if (dataload.ContainsKey("InitialLaunchVelocity")) {
					ingameman.initialstartforcemagnitude = float.Parse(dataload["InitialLaunchVelocity"]);
				}
				if (dataload.ContainsKey("ThrusterDuration")) {
					player.bouncedownDuration = float.Parse(dataload["ThrusterDuration"]);
				}
				if (dataload.ContainsKey("ThrusterCooldownTime")) {
					player.bounceRecoverTime = float.Parse(dataload["ThrusterCooldownTime"]);

				}
				if (dataload.ContainsKey("ThrusterForce_X")) {
					player.bouncedownEffect.x = float.Parse(dataload["ThrusterForce_X"]);

				}
				if (dataload.ContainsKey("ThrusterForce_Y")) {
					player.bouncedownEffect.y = float.Parse(dataload["ThrusterForce_Y"]);

				}
				if (dataload.ContainsKey("GroundSpeedReduction")) {
					player.groundhitDecayAmt = float.Parse(dataload["GroundSpeedReduction"]);

				}
				if (dataload.ContainsKey("BouncerSpeedReduction")) {
					player.bouncerhitDecayAmy = float.Parse(dataload["BouncerSpeedReduction"]);

				}
				if (dataload.ContainsKey("BouncerForce_X")) {
					player.bouncerupEffect.x = float.Parse(dataload["BouncerForce_X"]);

				}
				if (dataload.ContainsKey("BouncerForce_Y")) {
					player.bouncerupEffect.y = float.Parse(dataload["BouncerForce_Y"]);

				}
				if (dataload.ContainsKey("BouncersBaseFrequency")) {
					bouncers.baseBouncerChance = float.Parse(dataload["BouncersBaseFrequency"]);

				}
				if (dataload.ContainsKey("BouncersChanceDecreasePerHundredFt")) {
					bouncers.chanceDecreasePerHundredFeet = float.Parse(dataload["BouncersChanceDecreasePerHundredFt"]);

				}
				if (dataload.ContainsKey("DistanceToEarnOneCurrency")) {
					shop.disttogetcurrency = int.Parse(dataload["DistanceToEarnOneCurrency"]);

				}
				if (dataload.ContainsKey("SpeedBoostInitialCost")) {
					shop.speedboost_initialcost = int.Parse(dataload["SpeedBoostInitialCost"]);

				}
				if (dataload.ContainsKey("SpeedBoostCostIncreaseExponent")) {
					shop.speedboost_exponentincrease = float.Parse(dataload["SpeedBoostCostIncreaseExponent"]);

				}
				if (dataload.ContainsKey("ThrustersInitialCost")) {
					shop.thruster_initialcost = int.Parse(dataload["ThrustersInitialCost"]);

				}

				if (dataload.ContainsKey("ThrustersCostIncreaseExponent")) {
					shop.thruster_exponentincrease = float.Parse(dataload["ThrustersCostIncreaseExponent"]);

				}




			}
		}
	}

	public void ReportIn_NewDistPerRun(float val) {
		if (active) analyticsman.DistancePerRun.Add(val);
	}

	public void ReportIn_TotalNumberOfRuns(int val) {
		if (active) analyticsman.NumOfRuns = val;
	}
	public void ReportIn_TotalTimeInRun(float val) {
		if (active) analyticsman.TotalTimeInRuns = val;
	}

	public void ReportIn_TotalMoneyEarned(int val) {
	if(active) analyticsman.TotalMoneyEarned = val;
	}
	public void ReportIn_TotalMoneySpent(int val) {
		if (active) analyticsman.TotalMoneySpent = val;
	}
	public void ReportIn_SpeedUpgradesBought(int val) {
		if (active) analyticsman.SpeedUpgradesBought = val;
	}
	public void ReportIn_ThrusterUpgradesBought(int val) {
		if (active) analyticsman.ThrusterUpgradesBought = val;
	}
	public void ReportIn_ThrusterAttemptsTotal(int val) {
		if (active) analyticsman.ThrusterAttemptsTotal = val;
	}
	public void ReportIn_ThrusterInitiatedTotal(int val) {
		if (active) analyticsman.ThrusterInitiatedTotal = val;
	}
	public void ReportIn_BouncersHitTotal(int val) {
		if (active) analyticsman.BouncersHitTotal = val;
	}
	public void ReportIn_BouncersHitDuringThrust(int val) {
		if (active) analyticsman.BouncersHitDuringThrust = val;
	}
	public void ReportIn_HighestDistanceAchieved(int val) {
		if (active) analyticsman.HighestDistanceAchieved = val;
	}

}
