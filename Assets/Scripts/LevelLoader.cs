using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public GameObject buttonPrefab; 
    public Transform buttonsParent;
    public TMP_Text titleText;

    void Start()
    {
        LoadLevelData(2);
    }

    void LoadLevelData(int level)
    {
        TextAsset jsonData = Resources.Load<TextAsset>("Json/levels/level_" + level.ToString());
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData.text);

        titleText.text = levelData.title;

        foreach (Tile tile in levelData.tiles)
        {
            CreateButton(tile);
            Debug.Log(tile.position);
        }
    }

    void CreateButton(Tile tile)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, buttonsParent);
        buttonObj.GetComponentInChildren<TMP_Text>().text = tile.character;
        Vector3 position = new Vector3(tile.position.x, tile.position.y, tile.position.z);
        buttonObj.transform.localPosition = position;
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