using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static MainMenu instance;
    public bool isPaused;
    public GameObject pauseMenuUI;
    public GameObject loseUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpTime;
    public int score;
    private void Awake()
    {
        if (instance != null&&instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void LoseScreen()
    {
        loseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            pauseMenuUI.SetActive(false);
            loseUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreText.text = "Score: 0";
        StartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        loseUI.SetActive(false);
    }
}
