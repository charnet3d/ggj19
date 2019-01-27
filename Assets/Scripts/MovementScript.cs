using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;

    [SerializeField]
    float speed = 10;

    [SerializeField]
    float dashSpeed = 10.0f;

    [SerializeField]
    float maxDashTime = 10.0f;

    [SerializeField]
    float dashStoppingSpeed = 0.1f;

    Vector3 moveDirection;
    float currentDashTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        currentDashTime = maxDashTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire");
            currentDashTime = 0.0f;
        }

        if (currentDashTime < maxDashTime)
        {
            moveDirection = new Vector3(0, 0, dashSpeed);
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        rb.velocity += new Vector2(moveDirection.x, moveDirection.y) * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;

        if ((rb.velocity.x > 0 && tr.localScale.x < 0)
            || (rb.velocity.x < 0 && tr.localScale.x > 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        tr.localScale = new Vector2(tr.localScale.x * -1, tr.localScale.y);
    }
}
