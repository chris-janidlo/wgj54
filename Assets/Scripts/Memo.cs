using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : Clickable {

	public static event System.EventHandler MemoPickedUp;

	public Sprite ScreenImage;

	void Start () {
		MouseOverText = "(click) Pick Up";
	}

	public override void Click () {
		if (MemoPickedUp != null) {
			MemoPickedUp(this, null);
		}
		Destroy(gameObject);
	}

}
