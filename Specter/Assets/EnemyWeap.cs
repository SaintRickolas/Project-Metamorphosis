using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeap : MonoBehaviour {


	public float damage = 10f; 
	public float range = 100f; 

	public GameObject bullet; 

	public GameObject enemyWeapon; 


	public void shoot ()
	{
		 
		Instantiate
	}
	
		void OntriggerEnter ()
		{
			Target target = hit.transform.GetComponent<Target> ();
			if (target != null)
			{
				target.takeDamage (damage); 
			}
		}





}
