using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallSection : MonoBehaviour {

	public static readonly float ZLength = 2;

	public event EventHandler TriggerHandler;

	void OnTriggerEnter (Collider other) {
		if (other.tag != "Player") return;
		
		if (TriggerHandler != null) {
			TriggerHandler(this, null);
		}
	}

}
