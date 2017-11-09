using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMovement : MonoBehaviour 
{


	public float moveSpeed = 10f;
	public float turnSpeed = 100f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 translation = moveSpeed * Time.deltaTime * (Input.GetAxis ("Horizontal") * Vector3.right + Input.GetAxis ("Vertical") * Vector3.forward);
		transform.Translate (translation, Space.World);

//		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (translation), turnSpeed * Time.deltaTime * translation.magnitude);
	}
}
