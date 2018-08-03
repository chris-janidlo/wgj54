using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour {

	public static CameraRayCast Instance;

	public LayerMask CollisionMask;
	public float Distance;

	public Clickable HoveredObject { get; private set; }

	void Start () {
		Instance = this;
	}
	
	void Update () {
		raycast();

		if (HoveredObject != null && Input.GetMouseButtonDown(0)) {
			HoveredObject.Click();
		}
	}

	void raycast () {
		RaycastHit hitInfo;
		
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, Distance, CollisionMask, QueryTriggerInteraction.Ignore)) {
			var clickable = hitInfo.collider.gameObject.GetComponent<Clickable>();

			if (clickable == null) {
				HoveredObject = null;
			}
			else if (clickable != HoveredObject) {
				HoveredObject = clickable;
			}
		}
		else {
			HoveredObject = null;
		}

	}

}
