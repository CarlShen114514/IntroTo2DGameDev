using UnityEngine;
using UnityEngine.UI;

public class DestinationTrigger : MonoBehaviour
{
    public GameObject canvasGameOver; // 通关界面的 Canvas
    public string gameOverMessage = "Victory!"; // 通关信息

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowVictoryCanvas();
        }
    }

    private void ShowVictoryCanvas()
    {
        Text victoryText = canvasGameOver.GetComponentInChildren<Text>();
        victoryText.text = gameOverMessage;
        Debug.Log(gameOverMessage);
        Debug.Log(victoryText.text);
        canvasGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
