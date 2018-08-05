using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Clickable {

	public static Door Instance;

	public string OpenPrompt, ClosePrompt;
	public bool Open;

	Animator animator;
	bool animating;

	void Start () {
		Instance = this;
		
		animator = transform.parent.GetComponent<Animator>();
		MouseOverText = OpenPrompt;
	}

	public override void Click () {
		StartCoroutine(doorRoutine(!Open));
	}

	public void TryCloseDoor () {
		if (!Open) return;
		StartCoroutine(doorRoutine(false));
	}

	IEnumerator doorRoutine (bool opensIfThisIsTrue) {
		if (animating) yield break;
		MouseOverText = "";

		animating = true;
		if (opensIfThisIsTrue) Open = true; // open the instant it isn't closed

		string stateName = opensIfThisIsTrue ? "Open" : "Close";
		animator.Play(stateName);
		yield return new WaitForEndOfFrame();
		yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));
		
		if (!opensIfThisIsTrue) Open = false; // only say you're closed when fully closed
		
		animating = false;

		MouseOverText = Open ? ClosePrompt : OpenPrompt;
	}

}
