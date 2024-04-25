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

    [SerializeField]private GameObject letterPrefab;
    public Transform lettersParent;
    [SerializeField]private TMP_Text titleText;

    [SerializeField]private Transform levelsParent;
    [SerializeField]private GameObject levelPrefab;
    private static List<string> allJsonLevels;

    public List<GameObject> allLetters = new List<GameObject>();

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

    public void CreateLetter()
    {
        foreach (Tile tile in levelData.tiles)
        {
            GameObject newLetter = Instantiate(letterPrefab, lettersParent);
            
            newLetter.GetComponent<Letter>().letterText.text = tile.character;
            newLetter.GetComponent<Letter>().letterId= tile.id;
            newLetter.GetComponent<Letter>().letterPos= new Vector3(tile.position.x, tile.position.y, tile.position.z);
            
            allLetters.Add(newLetter);

            foreach (var child in tile.children)
            {
                newLetter.GetComponent<Letter>().letterChildren.Add(child);
            }
            
            // foreach (var child in tile.children)
            // {
            //     for (int i = 0; i < allLetters.Count; i++)
            //     {
            //         if (child == allLetters[i].GetComponent<Letter>().letterId)
            //         {
            //             var currentChild = allLetters[i];
            //
            //             if (currentChild != this)
            //             {
            //                 newLetter.GetComponent<Letter>().letterChildren.Add(currentChild);   
            //             }
            //         }
            //     }
            // }

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