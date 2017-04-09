using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/Oscillate_Rotation")]
public class Oscillate_Rotation : MonoBehaviour {
	//This script will interpolate whatever its attached to between two points.
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize

	[Header("Origin & Destination")]
	[Range(0,180f)]public float rotateAmount;
	public float travelTime = 1f;
	public bool beginImmediately = true;

	enum rotateDirec {x,y,z};
	[SerializeField]  rotateDirec rotationAxis = rotateDirec.z;


	//enum oscillation_type {Sine, Square, Triangle, Custom}
	[Tooltip("What sort of wave to osccilate by")]
	[Header("Oscillation Type")]
	[SerializeField] Easing.oscillation_type oscillationType = Easing.oscillation_type.Sine;
	public AnimationCurve customOscillation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	//private Vector3 original_rotation;
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
				displacement = Easing.Oscillate_Sine (-rotateAmount, rotateAmount, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Square:
				displacement = Easing.Oscillate_Square (-rotateAmount, rotateAmount, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Triangle:
				displacement = Easing.Oscillate_Triangle (-rotateAmount, rotateAmount, travelTime, Time.time);
				break;
			case Easing.oscillation_type.Custom:
				displacement = Easing.Oscillate_Custom (-rotateAmount, rotateAmount, travelTime, Time.time, customOscillation);
				break;
			}


			float xamt = 0, yamt = 0, zamt = 0;
			if (rotationAxis == rotateDirec.x) xamt = displacement;
			if (rotationAxis == rotateDirec.y) yamt = displacement;
			if (rotationAxis == rotateDirec.z) zamt = displacement;

			transform.localRotation = Quaternion.Euler (new Vector3 (xamt, yamt, zamt));
		}
	}




	public void Begin(){
		moving = true;
		//original_rotation = transform.localEulerAngles;
	}

	public void Pause(){
		moving = false;
	}
	public void Resume(){
		moving = true;
	}



}