using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour {

	public GameObject player;
	public Material[] backgrounds;
	public float[] scrollSpeeds;
	Vector2[] savedTextureOffset;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		savedTextureOffset = new Vector2[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			savedTextureOffset[i] = backgrounds [i].GetTextureOffset ("_MainTex");
		}

		originalPos = transform.position;
		for (int i = 0; i < backgrounds.Length; i++) {
			Vector2 offset = new Vector2(0, savedTextureOffset[i].y);
			backgrounds[i].SetTextureOffset("_MainTex", offset);
		}

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (player.transform.position.x, originalPos.y, originalPos.z);

		for (int i = 0; i < backgrounds.Length; i++) {

			float multiplier = scrollSpeeds [i];
			float x = Mathf.Repeat(player.transform.position.x * -1f * multiplier, 1);

			Vector2 offset = new Vector2 (x, savedTextureOffset [i].y);
			backgrounds [i].SetTextureOffset ("_MainTex", offset);
		}

	}


}
