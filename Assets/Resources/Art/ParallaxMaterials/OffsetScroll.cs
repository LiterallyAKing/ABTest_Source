using UnityEngine;
using System.Collections;

public class OffsetScroll : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	MeshRenderer myrenderer;

	void Start () {
		myrenderer = GetComponent<MeshRenderer> ();
		savedOffset = myrenderer.sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update () {
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		myrenderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () {
		myrenderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
