using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/Oscillate_Scale")]
public class Oscillate_Scale : MonoBehaviour {
	//This script will interpolate whatever its attached to between two points.
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize

	[Header("Origin & Destination")]
	public float scaleMin = 0.5f;
	public float scaleMax = 1.5f;
	public float travelTime = 1f;
	public bool beginImmediately = true;
	[Space]
	public bool scaleX = true;
	public bool scaleY = true;
	public bool scaleZ = true;


	//enum oscillation_type {Sine, Square, Triangle, Custom}
	[Tooltip("What sort of wave to osccilate by")]
	[Header("Oscillation Type")]
	[SerializeField] Easing.oscillation_type oscillationType = Easing.oscillation_type.Sine;
	public AnimationCurve customOscillation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	private Vector3 original_scale;
	private bool moving;
	//private bool stopnextloop = false;




	void Start () {
		if (beginImmediately)
			Begin ();
	}


	void Update () {
		float displacement = 0;

		if (moving) {
			switch (oscillationType) {
			case Easing.oscillation_type.Sine:
				displacement = Easing.Oscillate_Sine (scaleMin, scaleMax, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Square:
				displacement = Easing.Oscillate_Square (scaleMin, scaleMax, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Triangle:
				displacement = Easing.Oscillate_Triangle (scaleMin, scaleMax, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Custom:
				displacement = Easing.Oscillate_Custom (scaleMin, scaleMax, travelTime, Time.time, customOscillation);
				break;
			}


			float xamt = 1, yamt = 1, zamt = 1;
			if (scaleX) xamt = displacement;
			if (scaleY) yamt = displacement;
			if (scaleZ) zamt = displacement;

			transform.localScale = new Vector3 (original_scale.x * xamt,original_scale.y * yamt, original_scale.z * zamt);
		}
	}




	public void Begin(){
		moving = true;
		original_scale = transform.localScale;
	}

	public void Pause(){
		moving = false;
	}
	public void Resume(){
		moving = true;
	}



}