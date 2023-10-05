using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
    public SpriteRenderer spriteRenderer;
    public float speed = 0.000001f;
    Vector2 bucket = new Vector2(-34.5f, 38f);
    public bool start = false;
    public bool StartMoving = false;
    Vector2 set = new Vector2();
    public bool startReturn = false;
    public bool giveBucket = false;

    void Start()
    {
        spriteRenderer.enabled = false;
    }

    public void beginChickening()
    {
        set = playerRB.position;
        set.y += 1.55f;
        rb.MovePosition(set);
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position == set)
        {
            StartMoving = true;
            if (set.x > -34.5f){
                gameObject.transform.localScale = new Vector3(-1,1,1);
            }
            spriteRenderer.enabled = true;

        }
        if (start && StartMoving)
        {
            if (!startReturn)
            {
                rb.position = Vector3.MoveTowards(rb.position, bucket, Time.deltaTime * speed);
            }
            if (rb.position == bucket)
            {
                if (set.x > -34.5f){
                    gameObject.transform.localScale = new Vector3(1,1,1);
                } else{
                    gameObject.transform.localScale = new Vector3(-1,1,1);
                }
                Destroy(GameObject.Find("Bucket").GetComponent("SpriteRenderer"));
                Destroy(GameObject.Find("Bucket"));
                startReturn = true;
            }
            if (startReturn){
                rb.position = Vector3.MoveTowards(rb.position, set, Time.deltaTime * speed);
                if (rb.position == set)
                {
                    start = false;
                    giveBucket = true;
                }
            }
        }
    }
}
