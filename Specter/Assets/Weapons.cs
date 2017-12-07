
using UnityEngine;

public class Weapons : MonoBehaviour 
{

	public float damage = 10f; 
	public float range = 100f; 



	public GameObject playerWeapon; 


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			shoot (); 
		}
			}

	void shoot ()
	{
		RaycastHit hit; 
		if (Physics.Raycast (playerWeapon.transform.position, playerWeapon.transform.forward, out hit, range)) 
		{
			Target target = hit.transform.GetComponent<Target> ();
			if (target != null)
			{
				target.takeDamage (damage); 
			}
		}

		Debug.DrawRay (playerWeapon.transform.position, playerWeapon.transform.forward * range);

	}

}

