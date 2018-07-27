using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour {

	public static event System.EventHandler MemoPickedUp;
	public static bool MouseHover { get; private set; }

	public Sprite ScreenImage;

	public void OnMouseDown () {
		if (MemoPickedUp != null) {
			MemoPickedUp(this, null);
		}
		MouseHover = false;
		Destroy(gameObject);
	}

	public void OnMouseEnter () {
		MouseHover = true;
	}

	public void OnMouseExit () {
		MouseHover = false;
	}

}
