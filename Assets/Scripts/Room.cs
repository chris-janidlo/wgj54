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

}
