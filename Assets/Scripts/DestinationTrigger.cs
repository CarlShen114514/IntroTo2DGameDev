using UnityEngine;
using UnityEngine.UI;

public class DestinationTrigger : MonoBehaviour
{
    public GameObject PanelGameOver; // 通关界面的 Panel
    public string gameOverMessage = "Victory!"; // 通关信息
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowVictoryPanel();
        }
    }
    private void ShowVictoryPanel()
    {
        Text victoryText = PanelGameOver.GetComponentInChildren<Text>();
        victoryText.text = gameOverMessage;
        Debug.Log(gameOverMessage);
        Debug.Log(victoryText.text);
        PanelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
