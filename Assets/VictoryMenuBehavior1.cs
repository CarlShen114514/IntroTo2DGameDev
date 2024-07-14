using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuBehavior : MonoBehaviour
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
        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     if (isPaused)
        //     {
        //         canvas.SetActive(false);
        //         isPaused = false;
        //         Time.timeScale = 1f;
        //     }
        //     else
        //     {
        //         canvas.SetActive(true);
        //         isPaused = true;
        //         Time.timeScale = 0f;
        //     }
        // }
    }

    public void Continue()
    {
        SceneManager.LoadScene("PortalScene");
    }

    public void Restart()
    {
        Debug.Log("加载restart逻辑");
        canvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f; // 恢复游戏时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加载当前场景
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
