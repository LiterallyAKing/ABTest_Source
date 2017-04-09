using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/Oscillate_Position")]
public class Oscillate_Position : MonoBehaviour {
	//This script will interpolate whatever its attached to between two points.
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize

	[Header("Origin & Destination")]
	public Vector3 fromPoint;
	public Vector3 toPoint;
	public float travelTime = 1f;
	public bool beginImmediately = true;

	//enum oscillation_type {Sine, Square, Triangle, Custom}
	[Tooltip("What sort of wave to osccilate by")]
	[Header("Oscillation Type")]
	[SerializeField] Easing.oscillation_type oscillationType = Easing.oscillation_type.Sine;
	public AnimationCurve customOscillation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	private Vector3 original_position;
	private bool moving;
	private bool stopnextloop = false;

	void Start () {
		if (beginImmediately)
			Begin ();
	}


	void Update () {
		Vector3 displacement = Vector3.zero;

		if (moving) {
			switch (oscillationType) {
			case Easing.oscillation_type.Sine:
				displacement = Easing.Oscillate_Sine (fromPoint, toPoint, travelTime, Time.time);
					break;
			case Easing.oscillation_type.Square:
				displacement = Easing.Oscillate_Square (fromPoint, toPoint, travelTime, Time.time);
					break;
			case Easing.oscillation_type.Triangle:
				displacement = Easing.Oscillate_Triangle (fromPoint, toPoint, travelTime, Time.time);
					break;
			case Easing.oscillation_type.Custom:
				displacement = Easing.Oscillate_Custom (fromPoint, toPoint, travelTime, Time.time, customOscillation);
					break;
			}

			transform.localPosition = original_position + displacement;
		}
		if (stopnextloop && displacement.magnitude <= 0.001f) {
			moving = false;
			Deactivate ();
		}
	}


	public void Initialize(Vector3 _fromPoint, Vector3 _toPoint, float _travelTime, bool _begin, Easing.oscillation_type _oscType){
		fromPoint = _fromPoint;
		toPoint = _toPoint;
		travelTime = _travelTime;
		beginImmediately = _begin;
		oscillationType = _oscType;
	}

	public void Begin(){
		moving = true;
		original_position = transform.localPosition;
	}
	public void End(bool immediately){
		if (immediately) {
			moving = false;
			Deactivate ();
		} else if (!immediately){
			stopnextloop = true;
		}
	}
	public void Pause(){
		moving = false;
	}
	public void Resume(){
		moving = true;
	}

	private void Deactivate(){
		Destroy (this);
		//this.enabled = false;
	}


}
