using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HannMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
    public float speed = 5f;
    public PlayerMovement player;
    public BoxCollider2D hallway;
    public bool GoToThing = false;
    public Rigidbody2D key;
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
            if (player.currentTrigger == hallway || player.currentTrigger.tag == "HallwayCollider" || player.currentTrigger.name.IndexOf("Hallway") == 0 || player.currentTrigger.name.IndexOf("Door") == 0)
                rb.position = Vector3.MoveTowards(rb.position, playerRB.position, Time.deltaTime * speed);
            else
                rb.position = new Vector2(-15f, 15f);

        } else if (GoToThing)
        {
            rb.position = Vector3.MoveTowards(rb.position, new Vector2(-26.5f, 16.5f), Time.deltaTime * speed);
        }
    }

    public void GoToPedestal()
    {
        GoToThing = true;
        key.position = rb.position;

    }
}

