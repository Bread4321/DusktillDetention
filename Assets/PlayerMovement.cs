using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;
    public float moveSpeed = 20f;
    public Collider2D currentTrigger = null;
    public ArrayList inventory = new ArrayList();
    public Camera[] cameras = new Camera[8];
    public ChickenMovement chicken;
    public Animator animator;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public GameObject lambrightRoomObjects;
    private bool physicsMovement = false;
    public AudioManager audioManager;
    public PauseMenuUI pauseMenuUI;
    public GameObject CubeOnPedestal;
    public HannMovement Hann;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }

        audioManager.Play("Scheming-Weasel", true);
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
                    } else if (currentTrigger.gameObject.name == "Flashlight")
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        inventory.Add(currentTrigger.gameObject.name);
                        Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                        if (currentTrigger.GetComponent("BoxCollider2D") != null)
                        {
                            Destroy(currentTrigger.GetComponent("BoxCollider2D"));
                        }
                        Destroy(currentTrigger);
                    } else if (currentTrigger.gameObject.name == "Water")
                    {
                        if (inventory.Contains("Bucket"))
                        {
                            inventory.Remove("Bucket");
                            inventory.Add("BucketWithWater");
                            Destroy(currentTrigger.GetComponent("SpriteRenderer"));
                            if (currentTrigger.GetComponent("BoxCollider2D") != null)
                            {
                                Destroy(currentTrigger.GetComponent("BoxCollider2D"));
                            }
                            Destroy(currentTrigger);
                        }
                    }else if (currentTrigger.gameObject.name == "Pedestal")
                    {
                        if (inventory.Contains("RubiksCube"))
                        {
                            inventory.Remove("RubiksCube");
                            SpriteRenderer spriteRenderer = (SpriteRenderer)CubeOnPedestal.GetComponent("SpriteRenderer");
                            spriteRenderer.sprite = Resources.Load<Sprite>("RubiksCube");
                            Hann.GoToPedestal();
                            Destroy(currentTrigger);
                        }
                    }
                    else if (currentTrigger.gameObject.name == "Fire")
                    {
                        if (inventory.Contains("BucketWithWater"))
                        {
                            inventory.Remove("BucketWithWater");
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
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    } else if (doorName == "StorageToHallway")
                    {
                        cameras[2].enabled = false;
                        cameras[3].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    }
                    else if (doorName == "HallwayToLambright")
                    {
                        cameras[1].enabled = false;
                        cameras[4].enabled = true;
                        physicsMovement = true;
                        rb.freezeRotation = false;
                        lambrightRoomObjects.SetActive(true);
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Suspense", true);
                    }
                    else if (doorName == "EggletonToHallway")
                    {
                        cameras[7].enabled = false;
                        cameras[5].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    }
                    else if (doorName == "HallwayToBarnes")
                    {
                        cameras[3].enabled = false;
                        cameras[6].enabled = true;
                         audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Scheming-Weasel", true);
                    }
                    else if (doorName == "HallwayToRivero")
                    {
                        cameras[5].enabled = false;
                        cameras[8].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Scheming-Weasel", true);
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
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Scheming-Weasel", true);
                    }
                    else if (doorName == "DoorToStorageRoom")
                    {
                        cameras[3].enabled = false;
                        cameras[2].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("FireSound", true);
                    }
                    else if (doorName == "LambrightToHallway")
                    {
                        cameras[4].enabled = false;
                        cameras[1].enabled = true;
                        physicsMovement = false;
                        rb.freezeRotation = true;
                        transform.rotation = Quaternion.identity;
                        lambrightRoomObjects.SetActive(false);
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    }
                    else if (doorName == "DoorToEggletonRoom")
                    {
                        cameras[5].enabled = false;
                        cameras[7].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Scheming-Weasel", true);
                    }
                    else if (doorName == "BarnesToHallway")
                    {
                        cameras[6].enabled = false;
                        cameras[3].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    }
                    else if (doorName == "RiveroToHallway")
                    {
                        cameras[8].enabled = false;
                        cameras[5].enabled = true;
                        audioManager.Clear();
                        audioManager.Play("DoorOpen", false);
                        audioManager.Play("Sneaky-Snitch", true);
                    }
                } else if (string.Equals(currentTrigger.gameObject.name, "BreakerTrigger"))
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    globalLight.intensity = 1;
                    audioManager.Play("Switch", false);
                } else if (currentTrigger.gameObject.name == "FinalDoor" && inventory.Contains("Key"))
                {
                    audioManager.Clear();
                    Debug.Log("You win!");
                    pauseMenuUI.WinGame();
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
                audioManager.Clear();
                audioManager.Play("Lose", true);
                pauseMenuUI.GameOver();
            }
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);

        if (chicken.giveBucket)
        {
            chicken.giveBucket = false;
            inventory.Add("Bucket");
        }
    }

    void FixedUpdate()
    {
        if (physicsMovement)
        {
            rb.AddForce(movement, ForceMode2D.Impulse);
        } else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

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
