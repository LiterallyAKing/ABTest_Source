using UnityEngine;

public class Layer {

	public const int ignoreRaycast 		= 2;
	public const int red 				= 8;
	public const int blue 				= 9;
	public const int both 				= 10;

	public static LayerMask CreateInclusiveMask(int[] layers){
		int m = 0;
		for (int l = 0; l < layers.Length ; l++) {
			m |= (1<<layers[l]);
		}
		return m;
	}

	public static LayerMask CreateExclusiveMask(int[] layers){
		int m = 0;
		for (int l = 0; l < layers.Length;l++) {
			m |= (1<<layers[l]);
		}
		return ~m;
	}
}