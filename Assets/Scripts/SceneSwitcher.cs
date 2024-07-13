using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchNextScene();
            Debug.Log("RightArror Clicked.");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchPrevScene();
            Debug.Log("LeftArror Clicked.");
        }
    }
    
    public void SwitchPrevScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex != 0)
        {
            SceneManager.LoadScene(currentScene.buildIndex - 1);
        }
        
    }

    public void SwitchNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}