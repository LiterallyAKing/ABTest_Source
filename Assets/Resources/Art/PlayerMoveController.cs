using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerMoveController : MonoBehaviour {
	public AnalyticsPassThrough analyticspass;
	public Vector2 show_velocity;

	public GameObject groundHitHolder;
	public ParticleSystem hitparticles_right;
	public ParticleSystem hitparticles_left;

	//Variables
	public Vector2 startForce;
	[HideInInspector] public float startForceBoost = 1f;
	public float bounceRecoverTime;
	Timer bounceRecoverTimer;
	public float groundhitDecayAmt;
	public float bouncerhitDecayAmy;

	public AnimationCurve groundbounceCurve;
	public Vector2 groundbounceEffect;
	public float groundbounceDuration;
	public AnimationCurve bouncedownCurve;
	public Vector2 bouncedownEffect;
	public float bouncedownDuration;
	public AnimationCurve bouncerupCurve;
	public Vector2 bouncerupEffect;
	public float bouncerupDuration;

	//Internal references
	Rigidbody2D rb;
	float trueVelocityX = 0;
	float addlVelX = 0;
	float addlVelY = 0;
    Vector3 originalPos;

	//For nobouncezone
	bool amBouncing = false;
	public bool canBounce = false;
	public bool stopped = false;
	bool bouncingFromGround = false;


	int thrustersattempted = 0;
	int thrusterinitiated = 0;
	int bouncershittotal = 0;
	int bouncershitduringthrust = 0;


	public int maxBounces = 1;
	public int curBounces;

    //Look
    SpriteRenderer myrend;
	public GameObject spriteHolder;
    public Sprite normalSprite;
    public Sprite hitSprite;
	public Sprite endSprite;

    public GameObject bounceFlame;
	public GameObject goodHitEffect;

	public GameObject winceSprite;
	public GameObject sweatSprite;

	public void Init() {
		originalPos = transform.position;
		myrend = spriteHolder.GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		bounceRecoverTimer = new Timer(bounceRecoverTime, true);
		
	}

	public void Go(){
		curBounces = maxBounces;
		bounceRecoverTimer.Restart();
        if (startForceBoost < 1f) startForceBoost = 1f;
		rb.velocity = new Vector2 (startForce.x * startForceBoost, startForce.y);
		trueVelocityX = rb.velocity.x;
	}

	
	public void CameToStop() {
		if (!stopped) {
			stopped = true;
			myrend.sprite = endSprite;
			sweatSprite.SetActive(true);
		}
	}

	public void ResetForNewLaunch() {
		transform.position = originalPos;
		curBounces = maxBounces;
		canBounce = false;
		stopped = false;
		myrend.sprite = normalSprite;
		sweatSprite.SetActive(false);
	}





	void Update() {
		show_velocity = rb.velocity;


		if (Input.GetKeyDown(KeyCode.Space)) {
			thrustersattempted++;
			analyticspass.ReportIn_ThrusterAttemptsTotal(thrustersattempted);
			if (!amBouncing && canBounce && curBounces > 0) {
				curBounces--;
				amBouncing = true;
				thrusterinitiated++;
				analyticspass.ReportIn_ThrusterInitiatedTotal(thrusterinitiated);
				StartCoroutine("BounceDown");

			}
		}
		if (bounceRecoverTimer.IsFinished()) {
			curBounces++;
			curBounces = Mathf.Clamp(curBounces, 0, maxBounces);
		}


		float yAmt = 0;
		if (addlVelY != 0) {
			yAmt = addlVelY;
		} else {
			yAmt = rb.velocity.y;
		}
		rb.velocity = new Vector2(trueVelocityX + addlVelX, yAmt);
	}



	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			ongroundcount = 0;
			groundHitHolder.transform.position = new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z);
			hitparticles_right.Play();
			hitparticles_left.Play();

			if (amBouncing) {
				amBouncing = false;
				trueVelocityX -= groundhitDecayAmt / 4f;
			} else {
				trueVelocityX -= groundhitDecayAmt;
			}
			trueVelocityX = Mathf.Clamp(trueVelocityX, 0, 200f);

			StartCoroutine("GroundBounce");


		}
	}

	int ongroundcount = 0;
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			ongroundcount++;
			if (ongroundcount > 10) {
				if (amBouncing) {
					amBouncing = false;
					trueVelocityX -= groundhitDecayAmt / 4f;
				} else {
					trueVelocityX -= groundhitDecayAmt;
				}
				trueVelocityX = Mathf.Clamp(trueVelocityX, 0, 200f);

				StartCoroutine("GroundBounce");
			}

		}
	}

	IEnumerator BounceDown(){
        bounceFlame.SetActive(true);
		for (float time = 0f; time < bouncedownDuration; time += Time.deltaTime) {
			if (amBouncing) {
				float val = bouncedownCurve.Evaluate (time/bouncedownDuration);
				addlVelX = val * bouncedownEffect.x;
				addlVelY = val * bouncedownEffect.y;
				yield return null;
			} else {
				StopCoroutine ("BounceDown");
                bounceFlame.SetActive(false);
            }
		}
		amBouncing = false;
        bounceFlame.SetActive(false);
    }

	
	IEnumerator GroundBounce(){
		bouncingFromGround = true;
		StopCoroutine ("BounceDown");
        bounceFlame.SetActive(false);
        myrend.sprite = hitSprite;
		winceSprite.SetActive(true);
		for (float time = 0f; time < groundbounceDuration; time += Time.deltaTime) {
			float val = groundbounceCurve.Evaluate (time/groundbounceDuration);
			addlVelX = val * groundbounceEffect.x;

			float ymod = trueVelocityX / startForce.x;


			addlVelY = ymod* val * groundbounceEffect.y;

			yield return null;
		}
		bouncingFromGround = false;
		myrend.sprite = normalSprite;
		winceSprite.SetActive(false);
        addlVelX = 0;
		addlVelY = 0;
	}

	public IEnumerator BouncerBounce(){
		if (bouncingFromGround) yield break;
		bouncershittotal++;
		analyticspass.ReportIn_BouncersHitTotal(bouncershittotal);
		if (amBouncing) {
			bouncershitduringthrust++;
			analyticspass.ReportIn_BouncersHitDuringThrust(bouncershitduringthrust);
		}

		amBouncing = false;
		StopCoroutine ("BounceDown");

		goodHitEffect.SetActive(true);
		DOTween.Kill(goodHitEffect);
		goodHitEffect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		goodHitEffect.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 1f).OnComplete(() => goodHitEffect.SetActive(false));

		bounceFlame.SetActive(false);
        for (float time = 0f; time < bouncerupDuration; time += Time.deltaTime) {
			float val = groundbounceCurve.Evaluate (time/bouncerupDuration);
			addlVelX = val * bouncerupEffect.x;

			float ymod = trueVelocityX / startForce.x;


			addlVelY = ymod* val * bouncerupEffect.y;

			yield return null;
		}
		addlVelX = 0;
		addlVelY = 0;
	}





	public IEnumerator FadeOutGoodBounceEffect() {
	
		goodHitEffect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		
		return null;
	}





	//void OnDrawGizmos(){
	//	if (Application.isPlaying) {
	//		Gizmos.color = Color.white;
	//		Vector3 velo = new Vector3 (rb.velocity.x, rb.velocity.y, 0);
	//		Gizmos.DrawLine (transform.position, transform.position + velo);
	//	}
	//}


}
