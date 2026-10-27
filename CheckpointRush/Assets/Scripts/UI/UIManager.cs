using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.VectorGraphics;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text checkpointText;
    [SerializeField] private GameObject EndGamePanel;
    [SerializeField] private TMP_Text endMessage; 
    
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time left : " + gameManager.Timer;
        checkpointText.text = "Checkpoint : " + gameManager.CurrentCheckpointIndex + " / 5";

        if (gameManager.IsGameOver)
        {
            EndGamePanel.SetActive(true);

            if (gameManager.IsAWin)
            {
                endMessage.text = "You WIN with " + gameManager.Timer + " seconds left !";
            }
            else
            {
                endMessage.text = "You lose with " + (5 - gameManager.CurrentCheckpointIndex) + "checkpoint remaining";
            }
        }
    }

    // Quit Button
    public void Quit()
    {
        Application.Quit();
    }
}
