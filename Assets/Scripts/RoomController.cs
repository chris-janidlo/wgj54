using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public List<Room> RoomPrefabs; // TODO: pool these
	public Room Bedroom;

	public RoomTrigger BackTrigger, FrontTrigger;

	public HallController HallController;

	ZDir currentRoomDir = ZDir.Negative; // define the direction of the room as the direction you face when walking into it
	int nextRoomIndex = 0;

	Room currentRoom, waitingRoom;
	RoomTrigger currentRoomTrigger, waitingRoomTrigger;

	bool nextIsBedroom, hallInitialized, doorTriggered, doorClosing;

	void Start () {
		Memo.MemoPickedUp += onMemoPickedUp;

		currentRoom = Room.Initialize(Bedroom, currentRoomDir);
		waitingRoom = Room.Initialize(RoomPrefabs[nextRoomIndex], currentRoomDir.Flipped());

		currentRoomTrigger = BackTrigger;
		waitingRoomTrigger = FrontTrigger;
	}
	
	void Update () {
		if (RoomPrefabs.Count == 0 && !nextIsBedroom) {
			if (!hallInitialized) {
				HallController.Initialize(currentRoomDir.Flipped());
				hallInitialized = true;
			}
			return;
		}

		if (doorClosing) return;

		if (waitingRoomTrigger.PlayerExit && !doorTriggered) {
			StartCoroutine(roomSwitch());
		}
	}

	// this REQUIRES that the memos are placed in such a way that players can NEVER pick them up from inside the trigger zone
	IEnumerator roomSwitch () {
		// close door
		doorClosing = true;
		Door.Instance.TryCloseDoor();
		yield return new WaitWhile(() => Door.Instance.Open);
		doorClosing = false;
		
		// switch rooms
		Destroy(currentRoom.gameObject);
		currentRoom = waitingRoom;
		currentRoomDir = currentRoomDir.Flipped();

		nextRoomIndex = (int) Mathf.Repeat(nextRoomIndex + 1, RoomPrefabs.Count);
		var nextPrefab = nextIsBedroom ? Bedroom : RoomPrefabs[nextRoomIndex];
		waitingRoom = Room.Initialize(nextPrefab, currentRoomDir.Flipped());

		nextIsBedroom = false;

		currentRoomTrigger = currentRoomDir.IsPositive() ? FrontTrigger : BackTrigger;
		waitingRoomTrigger = currentRoomDir.IsNegative() ? FrontTrigger : BackTrigger;
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

	void onMemoPickedUp (object o, System.EventArgs e) {
		if (RoomPrefabs.Count == 0) return;
		
		nextIsBedroom = true;
		nextRoomIndex = (int) Mathf.Repeat(nextRoomIndex - 1, RoomPrefabs.Count);
		RoomPrefabs.Remove(RoomPrefabs[nextRoomIndex]);
	}

}
