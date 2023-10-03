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
    public Camera[] cameras = new Camera[8];
    public ChickenMovement chicken;
    // Start is called before the first frame update
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
                    if (currentTrigger.gameObject.name == "Bucket")
                    {
                        if (inventory.Contains("Egg"))
                        {
                            chicken.beginChickening();
                            inventory.Remove("Egg");
                        }
                    } else if (currentTrigger.gameObject.name == "Water")
                    {
                        if (inventory.Contains("Bucket"))
                        {
                            inventory.Remove("Bucket");
                            inventory.Add("BucketWithWater");
                            inventory.Add(currentTrigger.gameObject.name);
                            Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                            if (currentTrigger.GetComponent("BoxCollider2D") != null)
                            {
                                Destroy(currentTrigger.GetComponent("BoxCollider2D"));
                            }
                            Destroy(currentTrigger);
                        }
                    }
                    else if (currentTrigger.gameObject.name == "Fire")
                    {
                        if (inventory.Contains("BucketWithWater"))
                        {
                            Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                            if (currentTrigger.GetComponent("BoxCollider2D") != null)
                            {
                                Destroy(currentTrigger.GetComponent("BoxCollider2D"));
                            }
                            Destroy(currentTrigger);
                        }
                    }
                    else
                    {
                        inventory.Add(currentTrigger.gameObject.name);
                        Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                        if (currentTrigger.GetComponent("BoxCollider2D") != null)
                        {
                            Destroy(currentTrigger.GetComponent("BoxCollider2D"));
                        }
                        Destroy(currentTrigger);
                    }
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
                    } else if (doorName == "StorageToHallway")
                    {
                        cameras[2].enabled = false;
                        cameras[3].enabled = true;
                    }
                    else if (doorName == "HallwayToLambright")
                    {
                        cameras[1].enabled = false;
                        cameras[4].enabled = true;
                    }
                    else if (doorName == "EggletonToHallway")
                    {
                        cameras[7].enabled = false;
                        cameras[5].enabled = true;
                    }
                    else if (doorName == "HallwayToBarnes")
                    {
                        cameras[3].enabled = false;
                        cameras[6].enabled = true;
                    }
                    else if (doorName == "HallwayToRivero")
                    {
                        cameras[5].enabled = false;
                        cameras[8].enabled = true;
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
                    else if (doorName == "LambrightToHallway")
                    {
                        cameras[4].enabled = false;
                        cameras[1].enabled = true;
                    }
                    else if (doorName == "DoorToEggletonRoom")
                    {
                        cameras[5].enabled = false;
                        cameras[7].enabled = true;
                    }
                    else if (doorName == "BarnesToHallway")
                    {
                        cameras[6].enabled = false;
                        cameras[3].enabled = true;
                    }
                    else if (doorName == "RiveroToHallway")
                    {
                        cameras[8].enabled = false;
                        cameras[5].enabled = true;
                    }
                }
            }
            else if (currentTrigger.tag == "HallwayCollider")
            {
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
                else if (doorName == "MiddleToLeft")
                {
                    cameras[3].enabled = false;
                    cameras[5].enabled = true;
                }
                else if (doorName == "LeftToMiddle")
                {
                    cameras[5].enabled = false;
                    cameras[3].enabled = true;
                }
            }
            else if (currentTrigger.tag == "Teacher")
            {
                Debug.Log("You Died");
            }
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (chicken.giveBucket && !inventory.Contains("Bucket"))
        {
            inventory.Add("Bucket");
        }
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