using UnityEngine;
using System.Collections;

public class ManagerPersistence : AutoSingletonManager<ManagerPersistence> {
	public static ManagerPersistence instance;
//	public override void InitManager(){
//		DontDestroyOnLoad (gameObject);
//	}
//

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
