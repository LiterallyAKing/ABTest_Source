using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour {

	public float spinSpeed = 0;
	public PlayerMoveController playerControl;

	// Update is called once per frame
	void Update () {
		spinSpeed = playerControl.show_velocity.x;
		transform.Rotate(new Vector3(0,0, -spinSpeed));
	}
}
