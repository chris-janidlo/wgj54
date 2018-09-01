using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	bool spawned;
	public Room twin;

	// define the direction of the room as the direction you face when walking into it
	public static Room Initialize (Room room, ZDir direction) {
		room.spawned = true;

		var r = Instantiate(room);
		r.transform.eulerAngles = new Vector3(0, direction.IsNegative() ? 0 : 180, 0);
		return r;
	}

	// moves an already instantiated room
	public Room Respawn (ZDir direction) {
		switchLights(true);

		if (twin != null) return twin;
		
		if (spawned) {
			Room twinning = Initialize(this, direction);
			
			twinning.twin = this;
			twin = twinning;
			
			return twin;
		}
		
		spawned = true;

		transform.position = Vector3.zero;
		transform.eulerAngles = new Vector3(0, direction.IsNegative() ? 0 : 180, 0);

		return this;
	}

	// moves already instantiated room out of player view
	public void Despawn () {
		spawned = false;

		switchLights(false);

		if (twin == null) {
			transform.position = new Vector3(731, 731, 731);
		}
	}

	void switchLights (bool value) {
		foreach (var light in GetComponentsInChildren<Light>()) {
			light.enabled = value;
		}
	}

}
