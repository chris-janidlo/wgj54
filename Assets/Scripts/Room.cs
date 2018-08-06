using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	// define the direction of the room as the direction you face when walking into it
	public static Room Initialize (Room room, ZDir direction) {
		var r = Instantiate(room);
		r.transform.eulerAngles = new Vector3(0, direction.IsNegative() ? 0 : 180, 0);
		return r;
	}

	// moves an already instantiated room
	public Room Respawn (ZDir direction) {
		transform.position = Vector3.zero;
		transform.eulerAngles = new Vector3(0, direction.IsNegative() ? 0 : 180, 0);

		switchLights(true);
		return this;
	}

	// moves already instantiated room out of player view
	public void Despawn () {
		switchLights(false);
		transform.position = new Vector3(731, 731, 731);
	}

	void switchLights (bool value) {
		foreach (var light in GetComponentsInChildren<Light>()) {
			light.enabled = value;
		}
	}

}
