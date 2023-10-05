using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchultzMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2[] positions = new Vector2[2];
    public float speed = 5f;
    public float timePassed = 0f;
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector2(-1.448f, -8.5f);
        positions[1] = new Vector2(-1.448f, -0.6f);
        rb.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetInteger("State", moveState);
        timePassed += Time.deltaTime;
        if (timePassed < 2f){
            return;
        }
        rb.position = Vector3.MoveTowards(rb.position, positions[1], Time.deltaTime * speed);
        if (rb.position == positions[1])
        {
            Destroy(this.GetComponent("SpriteRenderer"));
            Destroy(this.GetComponent("CircleCollider2D"));
            Destroy(rb);
            this.enabled = false;
        }
    }
}
