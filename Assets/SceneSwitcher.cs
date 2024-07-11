using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{   
    public void SwitchPrevScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // 加载名为 "NextScene" 的场景
        SceneManager.LoadScene(currentScene.buildIndex - 1);
    }

    
    public void SwitchNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // 加载名为 "NextScene" 的场景
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}