using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Message Panel")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Clear Panel")]
    [SerializeField] private GameObject clearPanel;

    private int currentCoinCount = 0;
    private int totalCoinCount = 0;

    private void Start()
    {
        totalCoinCount = GameObject.FindGameObjectsWithTag("Coin").Length;

        UpdateCoinText();

        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void AddCoin()
    {
        currentCoinCount++;
        UpdateCoinText();

        if (IsAllCoinsCollected())
        {
            ShowMessage("모든 코인을 모았습니다!\nGoal에 도착하세요!");
        }
    }

    public void TryClearGame()
    {
        if (IsAllCoinsCollected())
        {
            GameClear();
        }
        else
        {
            ShowMessage("아직 코인을 다 먹지 않았습니다. 코인을 먹은 뒤 다시 오세요!");
        }
    }

    private bool IsAllCoinsCollected()
    {
        return currentCoinCount >= totalCoinCount;
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = $"Coin : {currentCoinCount} / {totalCoinCount}";
        }
    }

    private void ShowMessage(string message)
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
        }

        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void GameClear()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}