using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggletonMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2[] positions = new Vector2[4];
    public float speed = 5f;
    public int moveState = 0;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector2(-48f, -9f);
        positions[1] = new Vector2(-59f, -9f);
        positions[2] = new Vector2(-59f, -3f);
        positions[3] = new Vector2(-48f, -3f);
        rb.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("State", moveState);
        if (moveState == 0)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[1], Time.deltaTime * speed);
            if (rb.position == positions[1])
            {
                moveState++;
            }
        }
        else if (moveState == 1)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[2], Time.deltaTime * speed);
            if (rb.position == positions[2])
            {
                moveState++;
            }
        }
        else if (moveState == 2)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[3], Time.deltaTime * speed);
            if (rb.position == positions[3])
            {
                moveState++;
            }
        }
        else if (moveState == 3)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[0], Time.deltaTime * speed);
            if (rb.position == positions[0])
            {
                moveState = 0;
            }
        }

    }
}
