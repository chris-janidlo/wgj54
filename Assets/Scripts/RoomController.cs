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
		
		if (currentRoomTrigger.PlayerEnter && !doorTriggered) {
			var nextPrefab = nextIsBedroom ? Bedroom : RoomPrefabs[nextRoomIndex];
			waitingRoom = Room.Initialize(nextPrefab, currentRoomDir.Flipped());
		}

		if (currentRoomTrigger.PlayerExit && !doorTriggered) {
			StartCoroutine(destroyRoutine(true));
		}
		if (waitingRoomTrigger.PlayerExit && !doorTriggered) {
			// this REQUIRES that the memos are placed in such a way that players can NEVER pick them up from inside the trigger zone
			StartCoroutine(destroyRoutine(false));
		}
	}

	IEnumerator destroyRoutine (bool stillInCurrentRoom) {
		Door.Instance.TryCloseDoor();
		yield return new WaitWhile(() => Door.Instance.Open);
		
		if (stillInCurrentRoom) {
			Destroy(waitingRoom.gameObject);
		}
		else {
			Destroy(currentRoom.gameObject);
			currentRoom = waitingRoom;
			currentRoomDir = currentRoomDir.Flipped();
			nextRoomIndex = (int) Mathf.Repeat(nextRoomIndex + 1, RoomPrefabs.Count);

			nextIsBedroom = false;

			currentRoomTrigger = currentRoomDir.IsPositive() ? FrontTrigger : BackTrigger;
			waitingRoomTrigger = currentRoomDir.IsNegative() ? FrontTrigger : BackTrigger;
		}
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
