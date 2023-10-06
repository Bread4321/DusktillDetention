using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HannMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
    public float speed = 3f;
    public PlayerMovement player;
    public BoxCollider2D hallway;
    public bool GoToThing = false;
    public Rigidbody2D key;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb.position = new Vector2(-15f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentTrigger != null && !GoToThing)
        {
            if (player.currentTrigger == hallway || player.currentTrigger.tag == "HallwayCollider" || player.currentTrigger.name.IndexOf("Hallway") == 0 || player.currentTrigger.name.IndexOf("Door") == 0 || player.currentTrigger.name == "Pedestal" || player.currentTrigger.name == "Key")
            {
                rb.position = Vector3.MoveTowards(rb.position, playerRB.position, Time.deltaTime * speed);
                animator.SetFloat("X", playerRB.position.x-rb.position.x);
                animator.SetFloat("Y", playerRB.position.y-rb.position.y);
            }
            else
                rb.position = new Vector2(-15f, 15f);

        } else if (GoToThing)
        {
            animator.SetFloat("X", -1f);
            animator.SetFloat("Y", 0f);
            rb.position = Vector3.MoveTowards(rb.position, new Vector2(-26.5f, 15f), Time.deltaTime * speed);
        }
    }

    public void GoToPedestal()
    {
        GoToThing = true;
        key.position = rb.position;
    }
}
