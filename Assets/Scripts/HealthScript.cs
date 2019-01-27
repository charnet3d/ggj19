using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    Rigidbody2D rb;
    PathFollower pf;

    float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pf = GetComponent<PathFollower>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerBird")
        {
            health--;
        }

        if (health <= 0)
        {
            rb.simulated = true;
            pf.enabled = false;
        }
    }
}
