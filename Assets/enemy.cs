using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;
     public float speed = 3f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        // Set the destination of the NavMeshAgent to the player's position
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);

            // Raycast to detect obstacles in front of the enemy
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                // If obstacle detected, find a new random destination
                if (!hit.collider.CompareTag("Player"))
                {
                    // Find a random point within a certain range
                    Vector3 randomDirection = Random.insideUnitSphere * 5f;
                    randomDirection += transform.position;
                    NavMeshHit navHit;
                    NavMesh.SamplePosition(randomDirection, out navHit, 5f, NavMesh.AllAreas);
                    navMeshAgent.SetDestination(navHit.position);
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Game over logic (you can replace this with your own game over logic)
            Debug.Log("Game Over!");
            // For simplicity, we'll just quit the application
            Application.Quit();
        }
    }
     void OnTriggerEnter(Collider other)
    {
        // Check if the enemy collided with an object tagged "gome"
        if (other.CompareTag("gome"))
        {
            // Ignore collision with the object tagged "gome"
            Physics.IgnoreCollision(GetComponent<Collider>(), other.GetComponent<Collider>(), true);
        }
    }
}
