using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BounceText : MonoBehaviour {

	Text mytext;
	public PlayerMoveController player;

	// Use this for initialization
	void Start () {
		mytext = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		mytext.text = player.curBounces.ToString();
	}
}
