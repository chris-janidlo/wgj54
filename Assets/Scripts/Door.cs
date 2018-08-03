using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Clickable {

	public static Door Instance;

	public string OpenPrompt, ClosePrompt;

	Animator animator;
	bool open, animating;

	void Start () {
		animator = transform.parent.GetComponent<Animator>();
		MouseOverText = OpenPrompt;
	}

	public override void Click () {
		StartCoroutine(doorRoutine(!open));
	}

	public void TryCloseDoor () {
		if (!open) return;
		StartCoroutine(doorRoutine(false));
	}

	IEnumerator doorRoutine (bool opensIfThisIsTrue) {
		if (animating) yield break;
		MouseOverText = "";

		animating = true;

		string stateName = opensIfThisIsTrue ? "Open" : "Close";
		animator.Play(stateName);
		yield return new WaitForEndOfFrame();
		yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));
		
		open = opensIfThisIsTrue;
		
		animating = false;

		MouseOverText = open ? ClosePrompt : OpenPrompt;
	}

}
