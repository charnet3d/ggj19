using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;

    [SerializeField]
    float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Debug.Log(moveHorizontal);

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
