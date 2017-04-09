using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ShopManager : MonoBehaviour {

	public AnalyticsPassThrough analyticspass;
	public GameObject scientist;
	Vector3 scientist_originalPos = new Vector3(-765f, -250f, 0);
	Vector3 scientist_onPos = new Vector3(-315f, -250f, 0);
	Quaternion scientist_originalRot = Quaternion.Euler(new Vector3(0, 0, 70f));
	Quaternion scientist_onRot = Quaternion.Euler(new Vector3(0, 0, -18f));

	public GameObject blurfield;
	public int moneyAmt = 0;
	int totalmoneyearned = 0;
	public int speedBoostAmt = 0;
	public int thrustersAmt = 1;

	public int disttogetcurrency = 1;
	public int speedboost_initialcost = 50;
	public float speedboost_exponentincrease = 1.5f;
	public int thruster_initialcost = 300;
	public float thruster_exponentincrease = 2f;


	public Text moneyText;
	public Text speedText;
	public Text thrusterText;
	public Text speedbuttontext;
	public Text thrusterbuttonText;
	public InGameManager ingameman;

	public Text ingamethrust_txt, ingamethrust_num;
	public Text dist1, dist2, dist3;


	private void Awake() {
	}
	// Use this for initialization
	void Start () {
		blurfield.GetComponent<Renderer>().sortingOrder = 100;
		blurfield.GetComponent<Renderer>().sortingLayerName = "VizFX";
	}

	void OnEnable(){
        blurfield.SetActive(true);
		RefreshText ();
		scientist.transform.DOLocalMove(scientist_onPos, 1f);
		scientist.transform.DOLocalRotateQuaternion(scientist_onRot, 1.1f);
		ingamethrust_txt.CrossFadeAlpha(0f, 0.5f, true);
		ingamethrust_num.CrossFadeAlpha(0f, 0.5f, true);
		dist1.CrossFadeAlpha(0f, 0.5f, true);
		dist2.CrossFadeAlpha(0f, 0.5f, true);
		dist3.CrossFadeAlpha(0f, 0.5f, true);
	}


	void RefreshText(){
		moneyText.text = "$ " + moneyAmt.ToString ();
		speedText.text = (speedBoostAmt + ingameman.initialstartforcemagnitude).ToString ();
		thrusterText.text = (thrustersAmt).ToString();
		speedbuttontext.text = "Improve: " + "($" + speedboost_initialcost.ToString () + ")";
		thrusterbuttonText.text = "Improve: " + "($" + thruster_initialcost.ToString() + ")";
	}

	// Update is called once per frame
	void Update () {
	
	}


	public void PurchaseSpeed(){
		if (speedboost_initialcost <= moneyAmt) {
			moneyAmt -= speedboost_initialcost;
			analyticspass.ReportIn_TotalMoneySpent(totalmoneyearned - moneyAmt);

			speedBoostAmt++;
			analyticspass.ReportIn_SpeedUpgradesBought(speedBoostAmt);
			speedboost_initialcost = Mathf.RoundToInt(Mathf.Pow((float)speedboost_initialcost, speedboost_exponentincrease));

			RefreshText();
		}
	}
	public void PurchaseThruster() {
		if (thruster_initialcost <= moneyAmt) {
			moneyAmt -= thruster_initialcost;
			analyticspass.ReportIn_TotalMoneySpent(totalmoneyearned - moneyAmt);

			thrustersAmt++;
			analyticspass.ReportIn_ThrusterUpgradesBought(thrustersAmt-1);
			thruster_initialcost = Mathf.RoundToInt(Mathf.Pow((float)thruster_initialcost, thruster_exponentincrease));

			RefreshText();
		}
	}

	public void AddMoney(int amt){
		totalmoneyearned += (amt / disttogetcurrency);
		analyticspass.ReportIn_TotalMoneyEarned(totalmoneyearned);

		moneyAmt += (amt / disttogetcurrency);
	}

	public void CloseShop(){
        blurfield.SetActive(false);
		scientist.transform.DOLocalMove(scientist_originalPos, 1f);
		scientist.transform.DOLocalRotateQuaternion(scientist_originalRot, 1.1f);
		ingamethrust_txt.CrossFadeAlpha(1f, 0.5f, true);
		ingamethrust_num.CrossFadeAlpha(1f, 0.5f, true);
		dist1.CrossFadeAlpha(1f, 0.5f, true);
		dist2.CrossFadeAlpha(1f, 0.5f, true);
		dist3.CrossFadeAlpha(1f, 0.5f, true);
		ingameman.boostNum = speedBoostAmt;
		ingameman.player.maxBounces = thrustersAmt;
		ingameman.PrepareForLaunch ();
		this.gameObject.SetActive (false);
	}

}
