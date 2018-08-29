using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _makeVisibleInPlayMode : MonoBehaviour {

	new public Renderer renderer;

	void Start () {
		renderer.enabled = true;
	}
}
