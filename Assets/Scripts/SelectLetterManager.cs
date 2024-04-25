using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectLetterManager : MonoBehaviour
{
    public static SelectLetterManager Instance;
    
    [SerializeField]private float xOffset;

    [SerializeField]private int cellCount;
    [SerializeField]private GameObject cellPrefab;
    [SerializeField]private Transform cellParent;

    public List<Cell> cellList;
    public string completedWord;

    private void Awake()
    {
        Instance = this;

        ActionManager.Instance.OnCellsCreated += FindWhiteLettersCount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Letter letter = hit.collider.GetComponent<Letter>();
                if (letter != null)
                {
                    letter.SelectLetter();
                }
            }
        }
    }
    
    public void UpdateLetterStats()
    {
        foreach (var ltr in JsonFilesController.Instance.allLetters)
        {
            var letterChildren = ltr.GetComponent<Letter>().letterChildren;
            
            if (letterChildren.Count > 0)
            {
                foreach (var childId in letterChildren)
                {
                    foreach (var letter in JsonFilesController.Instance.allLetters)
                    {
                        if (childId == letter.GetComponent<Letter>().letterId)
                        {
                            var childLetter = letter.GetComponent<Letter>();
        
                            if (childLetter != this)
                            {
                                childLetter.GetComponent<MeshRenderer>().material.color = Color.gray;
                                childLetter.GetComponent<BoxCollider>().enabled = false;
                            }
                        }
                    }
                }
            }
        }
       
    }
    public void ResetLetters()
    {
        foreach (var letter in JsonFilesController.Instance.allLetters)
        {
            letter.GetComponent<MeshRenderer>().material.color = Color.white;
            letter.GetComponent<BoxCollider>().enabled = true;
        }
        
        UpdateLetterStats();
    }
    
    private IEnumerator FindWhiteLettersCountCo()
    {
        cellCount = 0;
        
        yield return new WaitForSeconds(.3f);
        
        for (int i = 0; i < JsonFilesController.Instance.allLetters.Count; i++)
        {
            if (JsonFilesController.Instance.allLetters[i].GetComponent<MeshRenderer>().material.color == Color.white)
            {
                cellCount++;

                if (cellCount > 7) cellCount = 7;
            }
        }

        CreateCell();
    }

    void FindWhiteLettersCount()
    {
        StartCoroutine(FindWhiteLettersCountCo());
    }

    private void CreateCell()
    {
        for (int i = 0; i < cellCount; i++)
        {
            Vector3 cellPos = new Vector3(cellParent.transform.position.x + xOffset, cellParent.transform.position.y, cellParent.transform.position.z);
            
            var newCell = Instantiate(cellPrefab,cellPos,Quaternion.identity,cellParent);

            cellList.Add(newCell.GetComponent<Cell>());
            
            xOffset += 9f;
        }
    }

    public void DestroyCells()
    {
        for (int i = 0; i < cellCount; i++)
        {
            Destroy(cellList[i].gameObject);
            
            xOffset -= 9f;
        }
        
        cellList.Clear();
    }

    public string CombineLetters()
    {
        completedWord = "";
        
        for (int i = 0; i < cellList.Count; i++)
        {
            completedWord += cellList[i].GetComponentInChildren<TMP_Text>().text;
        }

        return completedWord.ToLower();
    }
}
