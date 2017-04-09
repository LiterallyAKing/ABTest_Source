using UnityEngine;
using System.Collections;

public static class Easing {

	public enum easing_family {linear, quadratic, cubic, quartic, quintic, sine, exponential, circular, elastic, custom}
	public enum easing_type {ease_in, ease_out, ease_inout}
	/// <summary>
	/// Tweens a value, starting at _from by _delta amount over duration seconds
	/// </summary>
	public static float Tween(float _from, float _delta, float duration, float curtime, easing_family fam, easing_type type){
		if (curtime >= duration) return _from + _delta;
		else if (curtime <= 0) return _from;
		switch (fam) {
		case easing_family.linear:
			return Ease_Linear (_from, _delta, duration, curtime);
		case easing_family.quadratic:
			if (type == easing_type.ease_in) return EaseIn_Quadratic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Quadratic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Quadratic (_from, _delta, duration, curtime);
			break;
		case easing_family.cubic:
			if (type == easing_type.ease_in) return EaseIn_Cubic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Cubic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Cubic (_from, _delta, duration, curtime);
			break;
		case easing_family.quartic:
			if (type == easing_type.ease_in) return EaseIn_Quartic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Quartic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Quartic (_from, _delta, duration, curtime);
			break;
		case easing_family.quintic:
			if (type == easing_type.ease_in) return EaseIn_Quintic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Quintic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Quintic (_from, _delta, duration, curtime);
			break;
		case easing_family.sine:
			if (type == easing_type.ease_in) return EaseIn_Sine (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Sine (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Sine (_from, _delta, duration, curtime);
			break;
		case easing_family.exponential:
			if (type == easing_type.ease_in) return EaseIn_Exponential (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Exponential (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Exponential (_from, _delta, duration, curtime);
			break;
		case easing_family.circular:
			if (type == easing_type.ease_in) return EaseIn_Circular (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Circular (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Circular (_from, _delta, duration, curtime);
			break;
		case easing_family.elastic:
			if (type == easing_type.ease_in) return EaseIn_Elastic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_out) return EaseOut_Elastic (_from, _delta, duration, curtime);
			if (type == easing_type.ease_inout) return EaseInOut_Elastic (_from, _delta, duration, curtime);
			break;
		}
		return 0f;
	}
	public static float Tween(float _from, float _delta, float duration, float curtime, AnimationCurve curv, easing_type type){
		if (curtime >= duration) return _from + _delta;
		if (type == easing_type.ease_in) return EaseIn_Custom (_from, _delta, duration, curtime, curv);
		if (type == easing_type.ease_out) return EaseOut_Custom (_from, _delta, duration, curtime, curv);
		if (type == easing_type.ease_inout) return EaseInOut_Custom (_from, _delta, duration, curtime, curv);
		return 0f;
	}
	public static Vector3 Tween(Vector3 _from, Vector3 _to, float duration, float curtime, easing_family fam, easing_type type){
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn [i] = Tween (_from [i], _to [i], duration, curtime, fam, type);
		}
		return toReturn;
	}
	public static Vector3 Tween(Vector3 _from, Vector3 _to, float duration, float curtime, AnimationCurve curv, easing_type type){
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn [i] = Tween (_from [i], _to [i], duration, curtime, curv, type);
		}
		return toReturn;
	}

	private static float EaseIn_Elastic(float _from, float _delta, float dur, float time){
		time = time / dur;
		return _from + (_delta * ((56f * time * time * time * time * time) + (-105f * time * time * time * time) + (60f * time * time * time) + (-10f * time * time)));
	}
	private static float EaseOut_Elastic(float _from, float _delta, float dur, float time){
		time = time / dur;
		return _from + (_delta * ((56f * time * time * time * time * time) + (-175f * time * time * time * time) + (200f * time * time * time) + (-100f * time * time) + (20f * time)));
	}
	private static float EaseInOut_Elastic(float _from, float _delta, float dur, float time){
		time = time / (dur / 2f);
		if (time < 1f) return _from + (((_delta)/2f) * ((56f * time * time * time * time * time) + (-105f * time * time * time * time) + (60f * time * time * time) + (-10f * time * time)));
		time--;
		//Debug.Log (time);
		return (_from+(_delta/2f)) + ((_delta/2f) * ((56f * time * time * time * time * time) + (-175f * time * time * time * time) + (200f * time * time * time) + (-100f * time * time) + (20f * time)));
	}
	private static float EaseIn_Custom(float _from, float _delta, float dur, float time, AnimationCurve curv){
		time = time / dur;
		return ScaleFunc(_from, _delta, curv.Evaluate (time));
	}
	private static float EaseOut_Custom(float _from, float _delta, float dur, float time, AnimationCurve curv){
		time = time / dur;
		time = 1f - time;
		return ScaleFunc(_delta, _from, curv.Evaluate (time));
	}
	private static float EaseInOut_Custom(float _from, float _delta, float dur, float time, AnimationCurve curv){
		time = (time / (dur / 2f));
		if (time < 1f) return ScaleFunc(_from, _delta/2f, curv.Evaluate (time));
		time--;
		time = 1f - time;
		return ScaleFunc(_delta, _delta/2f, curv.Evaluate (time));
	}
	private static float Ease_Linear(float _from, float _delta, float dur, float time){
		return _delta * (time / dur) + _from;
	}
	private static float EaseIn_Quadratic(float _from, float _delta, float dur, float time){
		time = (time / dur);
		return (_delta * time * time) + _from; 
	}
	private static float EaseOut_Quadratic(float _from, float _delta, float dur, float time){
		time = time / dur;
		return (-_delta * time * (time - 2f)) + _from; 
	}
	private static float EaseInOut_Quadratic(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return ((_delta / 2f) * time * time) + _from;
		time--;
		return (-_delta / 2f) * (time * (time - 2f) - 1f) + _from;
	}
	private static float EaseIn_Cubic(float _from, float _delta, float dur, float time){
		time = (time / dur);
		return (_delta * time * time * time) + _from; 
	}
	private static float EaseOut_Cubic(float _from, float _delta, float dur, float time){
		time = time / dur;
		time--;
		return (_delta * (time * time * time + 1f)) + _from; 
	}
	private static float EaseInOut_Cubic(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return ((_delta / 2f) * (time * time * time)) + _from;
		time -= 2f;
		return (_delta / 2f) * ((time*time*time) + 2f) + _from;
	}
	private static float EaseIn_Quartic(float _from, float _delta, float dur, float time){
		time = (time / dur);
		return (_delta * time * time * time* time) + _from; 
	}
	private static float EaseOut_Quartic(float _from, float _delta, float dur, float time){
		time = time / dur;
		time--;
		return (-_delta * (time * time * time * time - 1f)) + _from; 
	}
	private static float EaseInOut_Quartic(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return ((_delta / 2f) * (time * time * time * time)) + _from;
		time -= 2f;
		return (_delta / 2f) * ((time*time*time*time) - 2f) + _from;
	}
	private static float EaseIn_Quintic(float _from, float _delta, float dur, float time){
		time = (time / dur);
		return (_delta * time * time * time * time * time) + _from; 
	}
	private static float EaseOut_Quintic(float _from, float _delta, float dur, float time){
		time = time / dur;
		time--;
		return (_delta * (time * time * time * time * time + 1f)) + _from; 
	}
	private static float EaseInOut_Quintic(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return ((_delta / 2f) * (time * time * time * time * time)) + _from;
		time -= 2f;
		return (_delta / 2f) * ((time*time*time*time*time) + 2f) + _from;
	}
	private static float EaseIn_Sine(float _from, float _delta, float dur, float time){
		return -_delta * Mathf.Cos ((time / dur) * (Mathf.PI / 2f)) + _delta + _from;
	}
	private static float EaseOut_Sine(float _from, float _delta, float dur, float time){
		return _delta * Mathf.Sin ((time / dur) * (Mathf.PI / 2f)) + _from;
	}
	private static float EaseInOut_Sine(float _from, float _delta, float dur, float time){
		return (-_delta/2f) * (Mathf.Cos ((time / dur) * Mathf.PI) -1f) + _from;
	}
	private static float EaseIn_Exponential(float _from, float _delta, float dur, float time){
		return _delta * Mathf.Pow (2f, 10f * ((time / dur) - 1f)) + _from;
	}
	private static float EaseOut_Exponential(float _from, float _delta, float dur, float time){
		return _delta * (Mathf.Pow (2f, -10f * ((time / dur))) + 1f) + _from;
	}
	private static float EaseInOut_Exponential(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return (_delta / 2f) * Mathf.Pow (2f, 10f * (time - 1f)) + _from;
		time--;
		return (_delta / 2f) * (-Mathf.Pow (2f, -10f * time) + 2f) + _from;
	}
	private static float EaseIn_Circular(float _from, float _delta, float dur, float time){
		time = time / dur;
		return -_delta * (Mathf.Sqrt (1f - (time * time)) - 1f) + _from;
	}
	private static float EaseOut_Circular(float _from, float _delta, float dur, float time){
		time = time / dur;
		time--;
		return _delta * Mathf.Sqrt (1f - (time * time)) + _from;
	}
	private static float EaseInOut_Circular(float _from, float _delta, float dur, float time){
		time = time / (dur/2f);
		if (time < 1f) return (-_delta / 2f) * (Mathf.Sqrt (1f - (time * time)) - 1f) + _from;
		time -= 2;
		return (_delta/2f) * (Mathf.Sqrt(1f - (time*time)) + 1f) + _from;
	}

		
	#region Oscillation functions
	public enum oscillation_type {Sine, Square, Triangle, Custom}
	/// <summary>
	/// Oscillates between two values along a sine wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static float Oscillate_Sine(float _from, float _to, float period, float t){
		return ScaleFunc (_from, _to, SineWave (period, t));
	}
	/// <summary>
	/// Oscillates between two values along square wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static float Oscillate_Square(float _from, float _to, float period, float t){
		return ScaleFunc (_from, _to, SquareWave (period, t));
	}
	/// <summary>
	/// Oscillates between two values along a linear triangle wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static float Oscillate_Triangle(float _from, float _to, float period, float t){
		return ScaleFunc (_from, _to, TriangleWave (period, t));
	}
	/// <summary>
	/// Oscillates between two values along an animation curve;
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	/// <param name="curv">Custom-defined curve</param>
	public static float Oscillate_Custom(float _from, float _to, float period, float t, AnimationCurve curv){
		curv.postWrapMode = WrapMode.PingPong;
		return ScaleFunc(_from, _to, curv.Evaluate (TriangleWave(period, t)));
	}
	/// <summary>
	/// Oscillates between two colors along a sine wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Color Oscillate_Sine(Color _from, Color _to, float period, float t){
		Color toReturn = new Color();
		for (int i = 0; i < 4; i++) {
			toReturn[i] = Oscillate_Sine(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two colors along square wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Color Oscillate_Square(Color _from, Color _to, float period, float t){
		Color toReturn = new Color();
		for (int i = 0; i < 4; i++) {
			toReturn[i] = Oscillate_Square(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two colors along a linear triangle wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Color Oscillate_Triangle(Color _from, Color _to, float period, float t){
		Color toReturn = new Color();
		for (int i = 0; i < 4; i++) {
			toReturn[i] = Oscillate_Triangle(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two colors along an animation curve;
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	/// <param name="curv">Custom-defined curve</param>
	public static Color Oscillate_Custom(Color _from, Color _to, float period, float t, AnimationCurve curv){
		curv.postWrapMode = WrapMode.PingPong;
		Color toReturn = new Color();
		for (int i = 0; i < 4; i++) {
			toReturn[i] = ScaleFunc(_from[i], _to[i], curv.Evaluate (TriangleWave(period, t)));
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two points along square wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Vector3 Oscillate_Sine(Vector3 _from, Vector3 _to, float period, float t){
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn[i] = Oscillate_Sine(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two points along square wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Vector3 Oscillate_Square(Vector3 _from, Vector3 _to, float period, float t){
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn[i] = Oscillate_Square(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two points along a linear triangle wave
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	public static Vector3 Oscillate_Triangle(Vector3 _from, Vector3 _to, float period, float t){
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn[i] = Oscillate_Triangle(_from[i], _to[i], period, t);
		}
		return toReturn;
	}
	/// <summary>
	/// Oscillates between two points along an animation curve;
	/// </summary>
	/// <param name="_from">From this</param>
	/// <param name="_to">To this</param>
	/// <param name="period">Period in seconds</param>
	/// <param name="t">Time.time</param>
	/// <param name="curv">Custom-defined curve</param>
	public static Vector3 Oscillate_Custom(Vector3 _from, Vector3 _to, float period, float t, AnimationCurve curv){
		curv.postWrapMode = WrapMode.PingPong;
		Vector3 toReturn = new Vector3();
		for (int i = 0; i < 3; i++) {
			toReturn[i] = ScaleFunc(_from[i], _to[i], curv.Evaluate (TriangleWave(period, t)));
		}
		return toReturn;
	}

	//These are private functions which define a sine, square, and triangle waves between 0 and 1.
	const float TwoPI = Mathf.PI * 2f;
	private static float SineWave(float p, float t){ //Returns sine oscillation between 0 and 1 at time t, p is period
		return 1f- (0.5f + (0.5f* (Mathf.Cos(TwoPI * t * (1f/p)))));
	}
	private static float SquareWave(float p, float t){
		return (1f+ Mathf.Sign(0f - (0 + (1f * (Mathf.Cos (TwoPI * t * (1f / p)))))))/2f;
	}
	private static float TriangleWave(float p, float t){
		return Mathf.Abs(2f*((t/p) - Mathf.Floor((t /p) + (1f/2f))));
	}
	private static float ScaleFunc(float fromnum, float tonum, float x){
		return fromnum + (x * ((tonum - fromnum) / 1f));
	}
	#endregion





}
