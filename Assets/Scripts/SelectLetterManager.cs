using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLetterManager : MonoBehaviour
{
    public static SelectLetterManager Instance;
    
    public float xOffset;

    public int cellCount;
    public GameObject cellPrefab;
    public Transform cellParent;

    public List<Cell> cellList;

    private void Awake()
    {
        Instance = this;

        ActionManager.Instance.OnLettersCreated += FindWhiteLettersCount;
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

    private IEnumerator FindWhiteLettersCountCo()
    {
        yield return new WaitForSeconds(.2f);
        
        for (int i = 0; i < JsonFilesController.Instance.allLetters.Count; i++)
        {
            if (JsonFilesController.Instance.allLetters[i].GetComponent<MeshRenderer>().material.color == Color.white)
            {
                cellCount++;
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
            
            xOffset += 10f;
        }
    }
}
