using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;
    public float moveSpeed = 20f;
    Collider2D currentTrigger = null;
    public ArrayList inventory = new ArrayList();
    public Camera[] cameras = new Camera[2];
    // public Light2D 

    void Start()
    {
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }

    void Update()
    {
        if (currentTrigger != null)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (currentTrigger.tag == "Collectable")
                {

                    inventory.Add(currentTrigger.gameObject.name);

                    if (string.Equals(currentTrigger.gameObject.name, "Flashlight"))
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }

                    Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                    Destroy(currentTrigger);


                } 
                else if (string.Equals(currentTrigger.gameObject.name, "BreakerTrigger"))
                {
                    
                }
                else if (currentTrigger.tag == "UpDoor")
                {
                    string doorName = currentTrigger.gameObject.name;
                    movement.x = 0;
                    movement.y = 11;
                    rb.position = rb.position + movement;
                    if (doorName == "SchultzToHallway")
                    {
                        cameras[0].enabled = false;
                        cameras[1].enabled = true;
                    }
                    else if (doorName == "StorageToHallway")
                    {
                        cameras[2].enabled = false;
                        cameras[3].enabled = true;
                    }
                    else if (doorName == "HallwayToLambright")
                    {
                        cameras[1].enabled = false;
                        cameras[4].enabled = true;
                    }
                }
                else if (currentTrigger.tag == "DownDoor")
                {
                    string doorName = currentTrigger.gameObject.name;
                    movement.x = 0;
                    movement.y = -11;
                    rb.position = rb.position + movement;
                    if (doorName == "DoorToSchultzRoom")
                    {
                        cameras[1].enabled = false;
                        cameras[0].enabled = true;
                    }
                    else if (doorName == "DoorToStorageRoom")
                    {
                        cameras[3].enabled = false;
                        cameras[2].enabled = true;
                    }
                }
            }
            else if (currentTrigger.tag == "HallwayCollider")
            {
                Debug.Log("Hallway");
                string doorName = currentTrigger.gameObject.name;
                if (doorName == "RightToMiddle")
                {
                    cameras[1].enabled = false;
                    cameras[3].enabled = true;
                }
                else if (doorName == "MiddleToRight")
                {
                    cameras[3].enabled = false;
                    cameras[1].enabled = true;
                }
            }
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        currentTrigger = null;
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        currentTrigger = collider;
    }

}