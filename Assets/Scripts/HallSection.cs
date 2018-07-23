using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallSection : MonoBehaviour {

	public static readonly float ZLength = 2;

	public GameObject Floor, Wall, Ceiling;
	public event EventHandler TriggerHandler;
	
	void Start () {
		makeMeshes();
		makeColliders();

	}

	void OnTriggerEnter (Collider other) {
		if (other.tag != "Player") return;
		
		if (TriggerHandler != null) {
			TriggerHandler(this, null);
		}
	}

	void makeColliders () {
		var trigger = gameObject.AddComponent<BoxCollider>();
		trigger.size = new Vector3(2, 2, .5f);
		trigger.center = Vector3.up;
		trigger.isTrigger = true;

		var floorColl = gameObject.AddComponent<BoxCollider>();
		floorColl.size = new Vector3(2.1f, 1, 2.1f);
		floorColl.center = Vector3.down / 2;

		var ceilColl = gameObject.AddComponent<BoxCollider>();
		ceilColl.size = new Vector3(2.1f, 1, 2.1f);
		ceilColl.center = Vector3.up * 2.5f;

		var wallCollR = gameObject.AddComponent<BoxCollider>();
		wallCollR.size = new Vector3(1, 2.1f, 2.1f);
		wallCollR.center = new Vector3(1.5f, 1, 0);

		var wallCollL = gameObject.AddComponent<BoxCollider>();
		wallCollL.size = new Vector3(1, 2.1f, 2.1f);
		wallCollL.center = new Vector3(-1.5f, 1, 0);
	}

	void makeMeshes () {
		// make transform.position the middle of the top face of the floor
		Instantiate(
			Floor,
			transform.position + Vector3.down * .2f,
			Floor.transform.rotation
		).transform.parent = transform;

		Instantiate(
			Ceiling,
			transform.position + Vector3.up * 2.2f,
			Ceiling.transform.rotation
		).transform.parent = transform;

		// x+
		Instantiate(
			Wall,
			transform.position + new Vector3(1.2f, 1, 0),
			Wall.transform.rotation * Quaternion.Euler(0, 0, 90)
		).transform.parent = transform;

		// x-
		Instantiate(
			Wall,
			transform.position + new Vector3(-1.2f, 1, 0),
			Wall.transform.rotation * Quaternion.Euler(0, 0, 90)
		).transform.parent = transform;
	}

}
