using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManeger : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TMP_Text  coinCountText;

    public static UIManeger Instance { get; set; }

    void Awake()
    {
        Time.timeScale = 0;

        if(Instance == null)
        {
            Instance=this;
        }
    }

    public void MyStart()
    {
        Time.timeScale = 1;
    }
    public void OpenGamePanel()
    {
        startGamePanel.SetActive(true);
    }
    public void CloseGamePanel()
    {
        startGamePanel.SetActive(false);
    }
    public void OpenStartPanel()
    {
        gamePanel.SetActive(true);
    }
    public void CloseStartPanel()
    {
        gamePanel.SetActive(false);
    }

    public void UpdateCoinValue(int coinCount)
    {
        coinCountText.text=coinCount.ToString();
    }
}
