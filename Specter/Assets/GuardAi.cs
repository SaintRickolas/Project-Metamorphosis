using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 



public class GuardAi : MonoBehaviour 
{

	public static event System.Action GuardHasSpottedPlayer;

	public Transform PathHolder; 
	public float speed;
	public float waitTime;
	public float  turnspeed;
	public float timeToSpot = .5f;
	float playerVisibleTimer; 

	public Light spotLight;
	public float viewDist; 
	float viewAngle; 
	public LayerMask viewMask; 
	Color originalSpotColor;

	public bool patrolling = true;


	private NavMeshAgent navAgent; //NAVMESH AGENT 
	public Transform target; 
	public Vector3 targetWaypoint;


	Transform Player;


	void Start () 

	{

		navAgent = GetComponent <NavMeshAgent> ();

		GameObject.FindGameObjectWithTag ("Audio"); 


		Player = GameObject.FindGameObjectWithTag ("Player").transform; 
		viewAngle = spotLight.spotAngle;
		originalSpotColor = spotLight.color; 


		Vector3 [] waypoints = new Vector3[PathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++) 
		{
			waypoints [i] = PathHolder.GetChild (i).position;
			waypoints [i] = new Vector3 (waypoints [i].x, transform.position.y,waypoints[i].z);
		}
	
		StartCoroutine (FollowPath (waypoints));
	} 



	void Update ()
	{
		if (CanSeePlayer ()) 
		{
			spotLight.color = Color.red; 
			playerVisibleTimer += Time.deltaTime;
			patrolling = false;
			navAgent.enabled = true;
		} 
		else 
		{
			//spotLight.color = originalSpotColor;
			playerVisibleTimer -= Time.deltaTime;
			patrolling = true;
			navAgent.enabled = true;


		}

		playerVisibleTimer = Mathf.Clamp (playerVisibleTimer,0,timeToSpot);
		spotLight.color = Color.Lerp(originalSpotColor, Color.red, playerVisibleTimer/timeToSpot); 


		if (playerVisibleTimer >= timeToSpot) //IF SPOTTED
		{



			if (GuardHasSpottedPlayer != null)
			{
//				GuardHasSpottedPlayer(); 
				navAgent.SetDestination (target.position);
				  

			}
		}
	}



	bool CanSeePlayer ()
	{
		if (Vector3.Distance (transform.position, Player.position) < viewDist) 
		{
			Vector3 dirToPlayer = (Player.position - transform.position).normalized;
			float angleBetween = Vector3.Angle (transform.forward, dirToPlayer);
			if (angleBetween < viewAngle / 2f) 
			{
				if (!Physics.Linecast (transform.position, Player.position, viewMask)) 
				{
					return true; 
				}
			}

		}

		return false; 
	}

	IEnumerator FollowPath(Vector3[] waypoints)   //Patrolling 
	{
		//transform.position = waypoints [0];

		int targetWaypointIndex = 1;
		targetWaypoint = waypoints [targetWaypointIndex];
		transform.LookAt (targetWaypoint);

		while (true)
		{
			if (patrolling) 
			{
				//transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, speed * Time.deltaTime);
				navAgent.destination = targetWaypoint; 
				//if (transform.position == targetWaypoint) 
				if (Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(targetWaypoint, Vector3.up)) < 0.01f) 
				{
					targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
					targetWaypoint = waypoints [targetWaypointIndex];
					yield return new WaitForSeconds (waitTime);
					yield return StartCoroutine (TurnToFace (targetWaypoint));
				}
			}

			yield return null;
		}

	} 


	IEnumerator TurnToFace(Vector3 lookAtTarget)
	{
		if (patrolling) {
			Vector3 dirtoTarget = (lookAtTarget - transform.position).normalized;
			float targetAngle = 90 - Mathf.Atan2 (dirtoTarget.z, dirtoTarget.x) * Mathf.Rad2Deg;


			while (Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.y, targetAngle)) > 0.05f) {
				float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, turnspeed * Time.deltaTime);
				transform.eulerAngles = Vector3.up * angle; 
				yield return null;
			}
		}

		yield return null;
	}


	void OnDrawGizmos ()
	{
		Vector3 startPosition = PathHolder.GetChild (0).position;
		Vector3 previousPosition = startPosition;
		foreach (Transform waypoint in PathHolder) 
		{
			Gizmos.DrawSphere (waypoint.position, .3f);
			Gizmos.DrawLine (previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);
		Gizmos.color = Color.red; 
		Gizmos.DrawRay (transform.position, transform.forward * viewDist);
	}
}
