using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoLightSwitch : MonoBehaviour {

	public Room parentRoom;

	void Start () {
		turnLights(false);
		Memo.MemoPickedUp += turnLightsOnEvent;
	}

	void turnLightsOnEvent (object o, System.EventArgs e) {
		if (parentRoom.IsCurrentRoom) {
			turnLights(true);
		}
	}

	void turnLights (bool value) {
		foreach (Light light in GetComponentsInChildren<Light>()) {
			light.enabled = value;
		}
	}
}
