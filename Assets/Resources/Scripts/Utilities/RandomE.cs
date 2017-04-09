using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RandomE {

	public static void Shuffle(this int[] toShuffle) 
	{
		for (int i = toShuffle.Length - 1; i >= 1; i--) {
			int j = Random.Range(0, i + 1);
			int tmp = toShuffle[i];
			toShuffle[i] = toShuffle[j];
			toShuffle[j] = tmp;
		}
	}
	public static void Shuffle<T>(this IList<T> list, int seed)
	{
		System.Random rng = new System.Random(seed);  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
	public static void Shuffle<T>(this IList<T> list)
	{
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}


	public static Vector3 RandomPoint(Bounds bounds) {
		return RandomPoint(bounds.min, bounds.max);
	}

	public static Vector3 RandomPoint(Vector3 min, Vector3 max) {
		return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	}

	public static Color RandomColor() {
		return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}


}
