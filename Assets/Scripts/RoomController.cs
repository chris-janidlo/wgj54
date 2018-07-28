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

	bool nextIsBedroom, doorTriggered, hallInitialized;

	void Start () {
		Memo.MemoPickedUp += onMemoPickedUp;

		currentRoom = Room.Initialize(Bedroom, currentRoomDir);

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
		
		if (currentRoomTrigger.PlayerEnter) {
			var nextPrefab = nextIsBedroom ? Bedroom : RoomPrefabs[nextRoomIndex];
			waitingRoom = Room.Initialize(nextPrefab, currentRoomDir.Flipped());
		}

		if (currentRoomTrigger.PlayerExit && !doorTriggered) {
			StartCoroutine(tryCloseDoor());

			Destroy(waitingRoom.gameObject);
		}
		if (waitingRoomTrigger.PlayerExit && !doorTriggered) {
			// this REQUIRES that the memos are placed in such a way that players can NEVER pick them up from inside the trigger zone

			StartCoroutine(tryCloseDoor());

			Destroy(currentRoom.gameObject);
			currentRoom = waitingRoom;
			currentRoomDir = currentRoomDir.Flipped();
			nextRoomIndex = (int) Mathf.Repeat(nextRoomIndex + 1, RoomPrefabs.Count);

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

	void onMemoPickedUp (object o, System.EventArgs e) {
		nextIsBedroom = true;
		nextRoomIndex = (int) Mathf.Repeat(nextRoomIndex - 1, RoomPrefabs.Count);
		RoomPrefabs.Remove(RoomPrefabs[nextRoomIndex]);
	}

}
