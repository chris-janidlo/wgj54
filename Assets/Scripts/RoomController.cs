using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public List<Room> RoomPrefabs; // TODO: pool these
	public Room Bedroom;
	public bool nextIsBedroom;

	public RoomTrigger BackTrigger, FrontTrigger;

	ZDir currentRoomDir = ZDir.Negative; // define the direction of the room as the direction you face when walking into it
	int nextRoomIndex = 0;

	Room currentRoom, waitingRoom;
	RoomTrigger currentRoomTrigger, waitingRoomTrigger;

	bool doorTriggered;

	void Start () {
		currentRoom = Room.Initialize(Bedroom, currentRoomDir);

		currentRoomTrigger = BackTrigger;
		waitingRoomTrigger = FrontTrigger;
	}
	
	void Update () {
		if (currentRoomTrigger.PlayerEnter) {
			var nextPrefab = nextIsBedroom ? Bedroom : RoomPrefabs[nextRoomIndex];
			waitingRoom = Room.Initialize(nextPrefab, currentRoomDir.Flipped());
		}

		if (currentRoomTrigger.PlayerExit && !doorTriggered) {
			StartCoroutine(tryCloseDoor());

			Destroy(waitingRoom.gameObject);
		}
		if (waitingRoomTrigger.PlayerExit && !doorTriggered) {
			StartCoroutine(tryCloseDoor());

			Destroy(currentRoom.gameObject);
			currentRoom = waitingRoom;
			currentRoomDir = currentRoomDir.Flipped();
			nextRoomIndex = (nextRoomIndex + 1) % RoomPrefabs.Count;

			nextIsBedroom = false;

			currentRoomTrigger = currentRoomDir.IsPositive() ? FrontTrigger : BackTrigger;
			waitingRoomTrigger = currentRoomDir.IsNegative() ? FrontTrigger : BackTrigger;
		}
	}

	IEnumerator tryCloseDoor () {
		// close door animation
		// yield return new WaitUntil(door is closed)
		yield return new WaitForEndOfFrame();
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			doorTriggered = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			doorTriggered = false;
		}
	}

}
