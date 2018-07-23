using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallController : MonoBehaviour {

	public HallSection HallObject;
	[Tooltip("Must be a positive odd number.")]
	public int MaxSections;

	Dictionary<int, HallSection> halls; // increasing index -> increasing z position
	int frontierPos = 0, frontierNeg = 0;

	int midpoint { get {
		return (frontierNeg + frontierPos) / 2;
	}}
	HallSection posTrigger { get {
		return halls[midpoint + 1];
	}}
	HallSection negTrigger { get {
		return halls[midpoint - 1];
	}}

	bool infinitized;

	public void Initialize (ZDir direction) {
		halls = new Dictionary<int, HallSection>(MaxSections);
		halls[0] = Instantiate(HallObject, transform.position, Quaternion.identity);
		halls[0].TriggerHandler += HallTriggerHandler;
		halls[0].transform.parent = transform;
		
		for (int i = 1; i < MaxSections; i++) {
			SpawnHall(direction);
		}
	}

	HallSection SpawnHall (ZDir direction) {
		Vector3 loc;
		if (direction == ZDir.Positive) {
			loc = halls[frontierPos].transform.position + Vector3.forward * HallSection.ZLength;
		}
		else {
			loc = halls[frontierNeg].transform.position + Vector3.back * HallSection.ZLength;
		}
		var hall = Instantiate(HallObject, loc, Quaternion.identity);
		hall.TriggerHandler += HallTriggerHandler;
		hall.transform.parent = transform;

		if (direction == ZDir.Positive) {
			halls[++frontierPos] = hall;
		}
		else {
			halls[--frontierNeg] = hall;
		}

		return hall;
	}
	
	void HallTriggerHandler (object o, System.EventArgs e) {
		HallSection h = (HallSection) o;

		if (!infinitized) {
			if (h == halls[midpoint])
				infinitized = true;
			else
				return;
		}

		if (h == posTrigger) {
			popLeft();
			SpawnHall(ZDir.Positive);
		}
		else if (h == negTrigger) {
			popRight();
			SpawnHall(ZDir.Negative);
		}
	}

	void popLeft () {
		Destroy(halls[frontierNeg].gameObject);
		halls[frontierNeg++] = null;
	}

	void popRight () {
		Destroy(halls[frontierPos].gameObject);
		halls[frontierPos--] = null;
	}

}
