using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class JsonFilesController : MonoBehaviour
{
    public static JsonFilesController Instance;
    
    public GameObject letterPrefab;
    public Transform lettersParent;
    public TMP_Text titleText;

    public Transform levelsParent;
    public GameObject levelPrefab;
    private static List<string> allJsonLevels;

    private TextAsset jsonData;
    private LevelData levelData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindAllJsonFiles();
    }

    private void FindAllJsonFiles()
    {
        string folderPath = "Assets/Resources/Json/levels";
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

        var jsonFiles = Directory.GetFiles(fullPath, "*.json", SearchOption.TopDirectoryOnly);

        allJsonLevels = jsonFiles.ToList();
    }

    public void ReadJsonFile(int level)
    {
        jsonData = Resources.Load<TextAsset>("Json/levels/level_" + level);
        levelData = JsonUtility.FromJson<LevelData>(jsonData.text);

        titleText.text = levelData.title;
    }

    public void CreateLevelsPanel()
    {
        PanelController.Instance.OpenLevelsPanel();

        for (int i = 0; i < allJsonLevels.Count; i++)
        {
            var newLevel = Instantiate(levelPrefab, levelsParent);

            LevelInfo newLevelInfo = newLevel.GetComponent<LevelInfo>();

            newLevelInfo.levelIndex = i + 1;
            newLevelInfo.levelText.text = "Level " + (i + 1);

            ReadJsonFile(i + 1);

            newLevelInfo.levelQuestion.text = titleText.text;
        }
    }

    public void CreateLevel()
    {
        foreach (Tile tile in levelData.tiles)
        {
            GameObject newLetter = Instantiate(letterPrefab, lettersParent);
            newLetter.GetComponentInChildren<TMP_Text>().text = tile.character;
            Vector3 position = new Vector3(tile.position.x, tile.position.y, tile.position.z);
            newLetter.transform.localPosition = position;
        }
    }
}


[System.Serializable]
public class LevelData
{
    public string title;
    public List<Tile> tiles;
}

[System.Serializable]
public class Tile
{
    public int id;
    public Position position;
    public string character;
    public List<int> children;
}

[System.Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}