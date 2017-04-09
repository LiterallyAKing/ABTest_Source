using UnityEngine;
using System.Collections;

public class BouncerMaker : MonoBehaviour {
	public PlayerMoveController player;

	public BouncerControl bouncePrefab;
	int bouncernumber = 0;
	int step = 8;
	public float baseBouncerChance = 0.8f;
	public float chanceDecreasePerHundredFeet = 0.15f;
	float lastpopulated = 0;

	void Start() {
		Random.InitState(1);

		//PopulateNext100Feet(0);

	}


	public void ResetForNewGame() {
		lastpopulated = 0;
		foreach (Transform child in this.transform) {
			Destroy(child.gameObject);
		}
	}

	public void NewGame() {
		lastpopulated = 0;
		PopulateNext100Feet(0);
	}



	private void Update() {
		if (player.transform.position.x > this.transform.position.x) {
			if (Vector3.Distance(this.transform.position, player.transform.position) > lastpopulated) {
				PopulateNext100Feet(lastpopulated + 100f);
			}
		}

	}


	void MakeBouncer(Vector3 pos) {
		BouncerControl newbouncer = Instantiate<BouncerControl>(bouncePrefab, this.transform);
		newbouncer.transform.localPosition = pos;
		float rand = Random.Range(0.9f, 1.1f);
		newbouncer.transform.localScale = new Vector3(rand, rand, rand);
		newbouncer.name = "Bouncer_" + bouncernumber;
		bouncernumber++;
	}


	void PopulateNext100Feet(float xstart) {
		lastpopulated = xstart;
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < 100; i += step) {
			float percentchance = Mathf.Clamp01(baseBouncerChance - ((xstart / 100f) * chanceDecreasePerHundredFeet) - (((float)i / 100f) * chanceDecreasePerHundredFeet));
			if (percentchance <= 0) return;

			if (Random.value < percentchance) {
				pos = new Vector3(xstart + (float)i, 0, 0);
				MakeBouncer(pos);
			}
		}
	}


}
