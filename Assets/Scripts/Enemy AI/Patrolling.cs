using System.Collections;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    // Storing our waypoints in an array
    public Transform[] waypoints; 
    
    //Setting movement speed
    [SerializeField] private float enemyMovementSpeed = 5f; 
    
    // Keeping track of the current waypoint we need to visit
    private int currentWaypointIndex = 0;
    
    // Checking if the enemy is currently moving
    private bool isMoving;

    void Update()
    {
        
        // Using the MoveTowards to move the enemy AI from one point to another at a constant speed
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, enemyMovementSpeed * Time.deltaTime);

        // Once we are in the finally reach the current waypoint we will call the funcition to switch the waypoint using a coroutine
        // We want to use coroutine so that we can add a delay before switching to the next waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f && !isMoving)
        {
            StartCoroutine(switchWaypoint());
        }
    }
    
    // Switching waypoint function
    IEnumerator switchWaypoint()
    {   
        // Had problems of multiple coroutine starts as it was updating to fast probably
        // Therefore, we keep track of wether or not the enemy is currently moving and prevent the update funciton from calling a coroutine again until we are done
        isMoving = true;  
         // Adding the delay before moving to the next target
         // This is very common in stealth games
        yield return new WaitForSeconds(5f); 

        // Updating the index and once we reach the end we will set it back to the very first index again
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        
        // Once done we reset it again so we can call corouties again once we reach the next waypoint
        isMoving = false;  
    }
}