using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMovement : MonoBehaviour 
{


	public float moveSpeed = 10f;
	public float turnSpeed = 100f;

	bool disabled;// for endgame!! TESTING ONLY 

	// Use this for initialization
	void Start () 
	{
		GuardAi.GuardHasSpottedPlayer += Disable;//Testing
	}
	
	// Update is called once per frame
	void Update ()
	{

		Vector3 inputDirection = Vector3.zero;//testing
		if (!disabled) //testing 
		{
			inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized; //testing
		}

		Vector3 translation = moveSpeed * Time.deltaTime * (Input.GetAxis ("Horizontal") * Vector3.right + Input.GetAxis ("Vertical") * Vector3.forward);
		transform.Translate (translation, Space.World);

//		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
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



