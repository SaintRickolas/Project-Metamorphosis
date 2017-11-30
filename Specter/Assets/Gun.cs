using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour
{


	public Transform spawn; 
	public float rpm;

	float effectiveDist = 20f; 
	public enum GunType {semi,burst,auto};  
	public GunType guntype; 


	private float secsBetweenShots;
	private float nextPossibleShot; 


	void start () 
	{
		secsBetweenShots = 60 / rpm;
	}

	public void Shoot () 

	{
		
		if (CanShoot())

		{
		Ray ray = new Ray (spawn.position, spawn.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray,out hit, effectiveDist))
		{
			effectiveDist = hit.distance;
		}

			nextPossibleShot = Time.time + secsBetweenShots;
			GetComponent<AudioSource> ().Play();
		}
		
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	


	public void AutoFire ()
	{
		if (guntype == GunType.auto)
		{
			Shoot ();
		}
			
	}


	private bool CanShoot () 

 	{
		bool canShoot = true; 


		if (Time.time < nextPossibleShot) 
		{
			canShoot = false;
		}

		return canShoot;
	}

		







}
