using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public int levelIndex;

    public TMP_Text levelText;
    public TMP_Text levelQuestion;
    public TMP_Text levelScore;

    [SerializeField]private Button startLevelButton;

    private void Start()
    {
        startLevelButton.onClick.AddListener(() => ActionManager.Instance.OnCellsCreated?.Invoke());
    }

    public void LoadLevel()
    {
        PanelController.Instance.OpenGamePanel();
        
        JsonFilesController.Instance.ReadJsonFile(levelIndex);
        JsonFilesController.Instance.CreateLetter();

    }

   
}
