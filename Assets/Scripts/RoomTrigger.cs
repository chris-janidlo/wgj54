using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoomTrigger : MonoBehaviour {

	public bool PlayerStay { get; private set; }
	public bool PlayerEnter { get; private set; }
	public bool PlayerExit { get; private set; }

	void Start () {
		GetComponent<Collider>().isTrigger = true;
	}

	void LateUpdate () {
		PlayerEnter = false;
		PlayerExit = false;
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			PlayerStay = true;
			PlayerEnter = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			PlayerStay = false;
			PlayerExit = true;
		}
	}

}
