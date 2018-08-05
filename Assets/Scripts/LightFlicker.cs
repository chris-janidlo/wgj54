using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

	public RandomRangeFloat OnTime, TransitionTime, OffTime;
	public Color OnColor, OffColor;

	Light l;

	void Start () {
		l = GetComponent<Light>();
		StartCoroutine(flickoroutine());
	}

	IEnumerator flickoroutine () {
		while (true) {
			l.color = OnColor;
			yield return new WaitForSeconds(OnTime.Value());

			float timer = 0, time = TransitionTime.Value();
			while (timer < time) {
				l.color = Color.Lerp(OnColor, OffColor, timer / time);
				timer += Time.deltaTime;
				yield return null;
			}

			yield return new WaitForSeconds(OffTime.Value());
		}
	}

}

[System.Serializable]
public class RandomRangeFloat {
	public float Min, Max;

	public float Value () {
		if (Min > Max)
			throw new System.Exception("Min (" + Min + ") is greater than Max (" + Max + ")");

		return Random.Range(Min, Max);
	}
}
