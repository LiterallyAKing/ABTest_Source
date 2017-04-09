using UnityEngine;
using System.Collections;

public class NoBounceZone : MonoBehaviour {

	public PlayerMoveController player;

	void OnTriggerExit2D(Collider2D col){
		player.canBounce = true;
	}
}
