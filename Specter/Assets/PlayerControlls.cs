using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour 
{

	public float speed = 6.0f;
	public float rotateSpeed  = 6.0f;
		
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate (speed * Time.deltaTime * Vector3.forward);
		}


		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate (speed * Time.deltaTime * Vector3.back);
		}


		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate (rotateSpeed * Time.deltaTime * Vector3.up);
		}



		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate (rotateSpeed * Time.deltaTime * Vector3.down);
		}



	}
}

