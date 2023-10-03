using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject gameOverScreen;
    public PlayerMovement playermovement;
    public ArrayList inventory = new ArrayList();


    void Start()
    {
        inventory = playermovement.inventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
            /*
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            */
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenInventory()
    {
        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(true);

        for (int i = 0; i < inventory.Count; i++)
        {
            // Must change item folder name to "Resources"
            Sprite s = Resources.Load<Sprite>(inventory[i].ToString());
            GameObject.Find("slot" + i).GetComponent<Image>().sprite = s;
            Debug.Log("Added: " + inventory[i] + " to slot" + i);
        }
    }

    public void BackButton()
    {
        inventoryUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void OpenSettings()
    {

    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}