using UnityEngine;
using System.Collections;

public class TerrainScroll : MonoBehaviour {

	public GameObject player;
	float width = 500f;
	bool loadednext = false;
	public GameObject prefab;


	void Start () {
		player = GameObject.Find ("Player_Bounce");
		width = GetComponent<SpriteRenderer> ().bounds.size.x;
	}

	void Update(){
		if ((player.transform.position.x >= transform.position.x) && !loadednext) {
			GameObject next = Instantiate (Resources.Load("Art/ParallaxMaterials/MainTerrain", typeof(GameObject)), transform.position + new Vector3 (width, 0, 0), Quaternion.identity) as GameObject;
			next.transform.parent = transform.parent;
			loadednext = true;
		}

		if (player.transform.position.x >= (transform.position.x + (width * 1.5f))) {
			Destroy (this.gameObject);
		}
	}
	

}
