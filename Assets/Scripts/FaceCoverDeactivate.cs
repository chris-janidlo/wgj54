using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCoverDeactivate : MonoBehaviour {

	public int MemosTillDeactivated;

	int counter;

	void Start () {
		counter = MemosTillDeactivated;
		Memo.MemoPickedUp += onMemoPickedUp;
	}

	void onMemoPickedUp (object o, System.EventArgs e) {
		counter--;
		if (counter <= 0) {
			Destroy(gameObject);
		}
	}
}
