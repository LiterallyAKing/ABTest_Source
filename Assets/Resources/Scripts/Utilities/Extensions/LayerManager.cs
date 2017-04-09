using UnityEngine;
using System.Collections;

public static class LayerManager  {

	public static void SetLayers(this GameObject go, int layer, bool includeChildren) {
		go.layer = layer;
		if (includeChildren) {
			for (int i = 0; i < go.transform.childCount; i++) {
				go.transform.GetChild(i).gameObject.SetLayers(layer, includeChildren);
			}
		}
	}

	public static bool IsInLayerMask(GameObject obj, LayerMask mask){
		return mask == (mask | (1 << obj.layer));
	}
}
