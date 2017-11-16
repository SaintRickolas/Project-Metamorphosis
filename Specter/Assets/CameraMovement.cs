using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	//https://www.youtube.com/watch?v=JnRB_GDW6CU 6:45

	public float smooth = 1.5f; 
	private Transform player; 
	private Vector3 relCam; 
	private float relCamPosMag; 
	private Vector3 NewPos; 




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;  
		relCam = transform.position - player.position; 
		relCamPosMag = relCam.magnitude - 0.5f;

	}


	void FixedUpdate ()
	{
		Vector3 standardPos = player.position + relCam;
		Vector3 abovePos = player.position + Vector3.up * relCamPosMag;
		Vector3[] checkPoints = new Vector3[5];

		checkPoints [0] = standardPos;
		checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f); 
		checkPoints [2] = Vector3.Lerp (standardPos,  abovePos, 0.5f);
		checkPoints [3] = Vector3.Lerp (standardPos,  abovePos, 0.75f);
		checkPoints [4] = abovePos; 


		for (int i = 0; i < checkPoints.Length; i++)
		{
			if (viewingPosCheck (checkPoints [i])) 
			{
				break;

			}
		}

		transform.position = Vector3.Lerp (transform.position, NewPos, smooth * Time.deltaTime);
		SmoothLookAt (); 
	}

	bool viewingPosCheck(Vector3 checkPos) 
	{
		RaycastHit hit; 
		if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCamPosMag)) 
		{
			if (hit.transform != player) 
			{
				return false; 
			}
		}
			
		NewPos = checkPos;
		return true; 
	}

	void SmoothLookAt ()
	{
		Vector3 relPlayerPosition = player.position - transform.position; 
		Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}

}
