using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    Transform thisTransform;

    [SerializeField]
    float speed = 10.0f;

    [SerializeField]
    float dashSpeed = 40.0f;

    [SerializeField]
    float maxDashTime = 0.5f;

    [SerializeField]
    float dashStoppingSpeed = 0.06f;

    Vector2 currentMovement;
    Vector2 currentDash;
    float currentDashTime;

    Vector3 screenPos;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisTransform = GetComponent<Transform>();

        currentDashTime = maxDashTime;
    }
    private void Update()
    {
        // Movement vector
        currentMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;

        // Initiate dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentDash = new Vector2(dashSpeed * thisTransform.localScale.x, -(dashSpeed * 2 / 3));
            currentDashTime = 0.0f;
        }

        // Stop the player at the edges of the screen
        screenPos = Camera.main.WorldToScreenPoint(thisTransform.position);
        Vector2 newPos = Vector2.zero;

        // Calc new edge position
        if (screenPos.x < 0)
            newPos = Camera.main.ScreenToWorldPoint(new Vector2(0, screenPos.y));

        if (screenPos.y < 0)
            newPos = Camera.main.ScreenToWorldPoint(new Vector2(screenPos.x, 0));

        if (screenPos.x > Screen.width)
            newPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, screenPos.y));
        
        if (screenPos.y > Screen.height)
            newPos = Camera.main.ScreenToWorldPoint(new Vector2(screenPos.x, Screen.height));

        // Apply it
        if (newPos != Vector2.zero)
            thisTransform.position = newPos;
    }

    void Flip()
    {
        thisTransform.localScale = new Vector2(thisTransform.localScale.x * -1, thisTransform.localScale.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Advance dash time
        if (currentDashTime < maxDashTime)
            currentDashTime += dashStoppingSpeed;
        else
            currentDash = Vector3.zero;

        // Apply movement and dash
        thisRigidbody.velocity = currentMovement + currentDash;

        // Flip player sprite according to movement direction
        if ((thisRigidbody.velocity.x > 0 && thisTransform.localScale.x < 0) || (thisRigidbody.velocity.x < 0 && thisTransform.localScale.x > 0))
            Flip();
    }

}
