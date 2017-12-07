using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public float health = 50f; 


	public void takeDamage (float ammount)
	{
		health -= ammount; 
		if (health <= 0f) 
		{
			Die ();
		}
	}



	void Die ()
	{
		Destroy(gameObject);
	}
		

}