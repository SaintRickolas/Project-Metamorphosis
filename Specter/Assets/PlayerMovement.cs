using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float Speed = 7f; 
	public float smoothMoveTime = .1f;
	public float turnSpeed = 8f;


	float smoothInputMagnitude;
	float smoothMoveVelocity;
	float angle; 


	// Use this for initialization
	void Start () 
	{



	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		float inputMagnitude = inputDirection.magnitude;
		smoothInputMagnitude = Mathf.SmoothDamp (smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

		float targetAngle = Mathf.Atan2 (inputDirection.x, inputDirection.z) * Mathf.Rad2Deg; 
		transform.eulerAngles = Vector3.up * angle;
		angle = Mathf.LerpAngle (angle, targetAngle, Time.deltaTime * turnSpeed *inputMagnitude);


		transform.Translate (transform.forward * Speed * Time.deltaTime * smoothInputMagnitude, Space.World);
		
	}
}
