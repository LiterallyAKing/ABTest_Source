using UnityEngine;
using System.Collections;

public class BouncerControl : MonoBehaviour {

	Vector3 originalpos;
	float range;
	float speed;


	void Start(){
		originalpos = transform.position;
		range = Random.Range (0.25f, 1.5f);
		speed = Random.Range (0.5f, 2f);
	}

	void Update(){
		float rand = range - Mathf.PingPong (Time.time * speed, 2f*range);
		transform.position = originalpos + new Vector3 (rand, 0, 0);
	}

		void OnTriggerEnter2D(Collider2D coll){
			if (coll.gameObject.tag == "Player") {
			if (!coll.gameObject.GetComponent<PlayerMoveController>().stopped) {
				coll.gameObject.GetComponent<PlayerMoveController>().StartCoroutine("BouncerBounce");
			}

				//Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
				//Vector2 vel = rb.velocity;
//				vel.x += 3.5f;
//				vel.y += (6f);
				//rb.velocity = vel;
				//rb.AddForce (new Vector2 ((bounceForce / 1f), bounceForce), ForceMode2D.Impulse);
			}
		}
}