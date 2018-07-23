using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallMockStart : MonoBehaviour {

	public ZDir Direction;

	void Start () {
		GetComponent<HallController>().Initialize(Direction);
	}
	
}
