using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Letter : MonoBehaviour
{
    public int letterId;
    public Vector3 letterPos;
    public TMP_Text letterText;
    public List<int> letterChildren;

    public bool isActivated = false;
    public int targetCellIndex;

    private void Start()
    {
        UpdateChildrenLettersInitialColor();

        transform.localPosition = letterPos;
    }

    private void UpdateChildrenLettersInitialColor()
    {
        if (letterChildren.Count > 0)
        {
            foreach (var childId in letterChildren)
            {
                for (int i = 0; i < JsonFilesController.Instance.allLetters.Count; i++)
                {
                    if (childId == JsonFilesController.Instance.allLetters[i].GetComponent<Letter>().letterId)
                    {
                        var childLetter = JsonFilesController.Instance.allLetters[i].GetComponent<Letter>();

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

    public void SelectLetter()
    {
        isActivated = !isActivated;

        if (isActivated) // Harf etkinleştirildiğinde
        {
            for (int i = 0; i < SelectLetterManager.Instance.cellList.Count; i++)
            {
                if (!SelectLetterManager.Instance.cellList[i].isEmpty)
                {
                    targetCellIndex = i;
                    Transform targetCell = SelectLetterManager.Instance.cellList[i].transform;
                    transform.DOMove(targetCell.position, 0.3f).OnComplete(() =>
                    {
                        SelectLetterManager.Instance.cellList[targetCellIndex].isEmpty = true;
                    });
                    break;
                }
            }
        }
        else // Harf pasif hale getirildiğinde
        {
            if (targetCellIndex != -1)
            {
                transform.DOMove(letterPos, 0.3f).OnComplete(() =>
                {
                    SelectLetterManager.Instance.cellList[targetCellIndex].isEmpty = false;
                    targetCellIndex = -1; // Hücre endeksi sıfırlanıyor.
                });
            }
        }
    }
}