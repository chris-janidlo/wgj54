using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoUI : MonoBehaviour {

	public string PromptText, DismissText;

	public static bool ImageOnScreen { get; private set; }

	Text[] TextContainer;
	Image ImageContainer;

	bool pressed;

	void Start () {
		TextContainer = GetComponentsInChildren<Text>();
		ImageContainer = GetComponentInChildren<Image>();
		Memo.MemoPickedUp += onMemoPickedUp;
	}
	
	void Update () {
		setText(Memo.MouseHover ? PromptText : "");
		if (ImageContainer.sprite != null) {
			setText(DismissText);
			if (!pressed && Input.GetMouseButton(0)) {
				ImageContainer.sprite = null;
				ImageContainer.enabled = false;
				ImageOnScreen = false;
			}
		}
		else {
			ImageContainer.enabled = false;
		}

		if (!Input.GetMouseButton(0)) pressed = false;
	}

	void setText (string value) {
		foreach (Text text in TextContainer) {
			text.text = value;
		}
	}

	void onMemoPickedUp (object o, System.EventArgs e) {
		ImageContainer.sprite = ((Memo) o).ScreenImage;
		ImageContainer.enabled = true;
		ImageOnScreen = true;
		pressed = true;
	}

}
