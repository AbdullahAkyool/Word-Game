using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance;
    
    public GameObject mainMenuPanel;
    public GameObject levelsPanel;
    public GameObject gamePanel;
    
    private void Awake()
    {
        Instance = this;
    }

    public void OpenMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        levelsPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void OpenLevelsPanel()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(true);
        gamePanel.SetActive(false);
        
        
        SelectLetterManager.Instance.DestroyCells();
        CheckWord.Instance.ClearPanel();
    }

    public void OpenGamePanel()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}
