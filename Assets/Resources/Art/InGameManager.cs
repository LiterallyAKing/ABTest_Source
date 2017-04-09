using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour {

	public float initialstartforcemagnitude = 12f;

	public DistanceTracker disttrac;
	float oldscore;
	Vector3 startpos;

	public Launcher launcher;
	public PlayerMoveController player;
	public BouncerMaker bouncermaker;
	public ShopManager shop;
	bool inShopMode = false;

	bool firstlaunch = true;

	public int boostNum = 0;

	public AnalyticsPassThrough analyticspass;
	float timeinrun = 0;

	void Start () {

	}




	void Update () {
		if (!inShopMode) {
			timeinrun += Time.deltaTime;
		}
	}

	int numofruns = 0;
	public void PlayerHasStopped() {
		inShopMode = true;
		shop.AddMoney(Mathf.RoundToInt(disttrac.score));
		analyticspass.ReportIn_NewDistPerRun((float)Mathf.RoundToInt(disttrac.score));
		analyticspass.ReportIn_HighestDistanceAchieved(Mathf.RoundToInt(disttrac.score));
		analyticspass.ReportIn_TotalTimeInRun(timeinrun);
		numofruns++;
		analyticspass.ReportIn_TotalNumberOfRuns(numofruns);

		shop.gameObject.SetActive(true);

		player.ResetForNewLaunch();
		disttrac.Reset ();
		launcher.ResetForNextLaunch();
		//terrainscroll.enabled = false;
		player.gameObject.SetActive(false);
		
	}

	public void StartNewLaunch() {
		bouncermaker.NewGame();
		player.startForceBoost = (float)boostNum;
		player.Go ();
		disttrac.gameRunning = true;
	}


	public void PrepareForLaunch(){
		inShopMode = false;

		launcher.StartPreLaunch();
		bouncermaker.ResetForNewGame();
	}

	public void LaunchHit(Vector3 newplayerpos, float newplayerlaunchangle) {

		player.gameObject.SetActive(true);
		if (firstlaunch) {
			player.Init();
			firstlaunch = false;
		}

		//terrainscroll.enabled = true;
		player.transform.position = newplayerpos;

		float angleInRadians = newplayerlaunchangle * Mathf.Deg2Rad;
		Vector2 newstartforce = new Vector2((float)Mathf.Cos(angleInRadians), (float)Mathf.Sin(angleInRadians));
		newstartforce *= initialstartforcemagnitude;
		player.startForce = newstartforce;

		StartNewLaunch();
	}

}
