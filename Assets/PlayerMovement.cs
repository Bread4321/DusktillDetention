using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D collider;
    Vector2 movement;
    public float moveSpeed = 20f;
    Collider2D currentTrigger = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (currentTrigger != null)
            {
                Destroy(currentTrigger.GetComponent("SpriteRenderer"));
            }
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        currentTrigger = collider;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        currentTrigger = null;
    }
}