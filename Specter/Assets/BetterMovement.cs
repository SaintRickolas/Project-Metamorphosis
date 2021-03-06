﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMovement : MonoBehaviour 
{


	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	public Gun gun; 

	bool disabled;// for endgame!! TESTING ONLY 

	CharacterController cc;

	// Use this for initialization
	void Start () 
	{
		GuardAi.GuardHasSpottedPlayer += Disable;//Testing

		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			gun.Shoot (); 
		}

		else if (Input.GetKey (KeyCode.Space))
			{
			//gun.AutoFire (); 
		}

		Vector3 inputDirection = Vector3.zero;//testing
		if (!disabled) //testing 
		{
			inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized; //testing
		}

		Vector3 translation = moveSpeed * Time.deltaTime * (Input.GetAxis ("Horizontal") * Vector3.right + Input.GetAxis ("Vertical") * Vector3.forward);
//		transform.Translate (translation, Space.World);

////		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
//		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
		if (translation.magnitude > 1e-4f)
		{
			// transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(translation), turnSpeed * Time.deltaTime * translation.magnitude);
		}

		cc.Move (translation);
	}

	void Disable () //testing
	{
		disabled = true; //testing 
	}



	void OnDestroy ()
	{
		GuardAi.GuardHasSpottedPlayer -= Disable; 	// testing
	}

}



