using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour {

	public static PlayerMove Instance;

	public float Speed;

	CharacterController cc;

	void Start () {
		Instance = this;
		cc = GetComponent<CharacterController>();
	}
	
	void Update () {
		var velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Speed;
		cc.SimpleMove(transform.TransformDirection(velocity));
	}

}
