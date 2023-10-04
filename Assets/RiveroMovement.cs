using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiveroMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform water;
    public Vector2[] positions = new Vector2[6];
    public float speed = 1f;
    public int moveState = 0;
    float timePassed = 0f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector2(-53.5f, 36.5f);
        positions[1] = new Vector2(-44f, 36.5f);
        positions[2] = new Vector2(-44f, 33f);
        positions[3] = new Vector2(-53.5f, 33f);
        positions[4] = new Vector2(-63f, 36.5f);
        positions[5] = new Vector2(-63f, 33f);
        rb.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("State", moveState);
        timePassed += Time.deltaTime;
        if (timePassed > 3f)
        {
            water.position = rb.position;
            timePassed = 0f;
        }

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
                moveState++;
            }
        }
        else if (moveState == 4)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[4], Time.deltaTime * speed);
            if (rb.position == positions[4])
            {
                moveState++;
            }
        }
        else if (moveState == 5)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[5], Time.deltaTime * speed);
            if (rb.position == positions[5])
            {
                moveState++;
            }
        }
        else if (moveState == 6)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[3], Time.deltaTime * speed);
            if (rb.position == positions[3])
            {
                moveState++;
            }
        }
        else if (moveState == 7)
        {
            rb.position = Vector3.MoveTowards(rb.position, positions[0], Time.deltaTime * speed);
            if (rb.position == positions[0])
            {
                moveState = 0;
            }
        }

    }
}
