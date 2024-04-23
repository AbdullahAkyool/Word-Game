using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int levelIndex;

    public TMP_Text levelText;
    public TMP_Text levelQuestion;
    public TMP_Text levelScore;

    public void LoadLevel()
    {
        PanelController.Instance.OpenGamePanel();
        
        JsonFilesController.Instance.ReadJsonFile(levelIndex);
        JsonFilesController.Instance.CreateLevel();
    }
}
