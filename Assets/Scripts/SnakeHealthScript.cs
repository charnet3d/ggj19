using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHealthScript : MonoBehaviour
{
    Rigidbody2D thisRigidBody;
    PathFollower pathFollower;
    Collider2D thisCollider;
    Animator thisAnimator;

    float health = 1;

    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        pathFollower = GetComponent<PathFollower>();
        thisCollider = GetComponent<Collider2D>();
        thisAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (other.name == "PlayerBird")
        {
            health--;
        }
        Debug.Log(health);

        if (health <= 0)
        {
            thisRigidBody.gravityScale = 1;
            thisCollider.isTrigger = false;
            pathFollower.enabled = false;
            thisAnimator.SetBool("walking", false);
        }

        if (thisCollider.isTrigger && other.name == "Ground")
        {
            thisRigidBody.gravityScale = 0;
            thisCollider.isTrigger = true;
        }
    }
}
