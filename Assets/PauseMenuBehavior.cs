using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPaused = false;
    private GameObject canvas = null;
    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
            isPaused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                canvas.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
            else
            {
                canvas.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
        }
    }

    public void Continue()
    {
        canvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
