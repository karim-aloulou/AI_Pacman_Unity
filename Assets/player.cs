using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody rb;
    public Text scoreText; 
    private int score = 0; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreText();
    }

    private void Update()
    {
        // Player movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Rotate player to face the movement direction
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        // Apply movement to the player
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Raycast to detect objects in front of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("gome"))
            {
                // Destroy the dot (pellet)
                Destroy(hit.collider.gameObject);
                Debug.Log("Dot eaten!");
                score++;
                Debug.Log(score);
                UpdateScoreText();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
         if (collision.collider.CompareTag("gome"))
        {
            // Destroy the dot (pellet)
            Destroy(collision.gameObject);
            Debug.Log("Dot eaten!");
             score++;
            Debug.Log(score);
            UpdateScoreText();

        }
        // Check if the player collides with a wall
        else if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("collider");
            // Reverse the player's movement to prevent passing through walls
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Get the direction from the player to the collision point
            
        }
    }
     private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}