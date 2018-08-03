using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoUI : MonoBehaviour {

	public string DismissPrompt;

	public static bool ImageOnScreen { get; private set; }

	Text[] texts;
	Image image;

	bool pressed;

	void Start () {
		texts = GetComponentsInChildren<Text>();
		image = GetComponentInChildren<Image>();
		Memo.MemoPickedUp += onMemoPickedUp;
	}
	
	void Update () {
		var hov = CameraRayCast.Instance.HoveredObject;
		setText(hov == null ? "" : hov.MouseOverText);

		if (image.sprite != null) {
			setText(DismissPrompt);
			if (!pressed && Input.GetMouseButton(0)) {
				image.sprite = null;
				image.enabled = false;
				ImageOnScreen = false;
			}
		}
		else {
			image.enabled = false;
		}

		if (!Input.GetMouseButton(0)) pressed = false;
	}

	void setText (string value) {
		foreach (Text text in texts) {
			text.text = value;
		}
	}

	void onMemoPickedUp (object o, System.EventArgs e) {
		image.sprite = ((Memo) o).ScreenImage;
		image.enabled = true;
		ImageOnScreen = true;
		pressed = true;
	}

}
