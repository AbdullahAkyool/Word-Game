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

    [SerializeField] private bool isActivated = false;
    [SerializeField] private int targetCellIndex;

    private void Start()
    {
        SelectLetterManager.Instance.UpdateLetterStats();

        transform.localPosition = letterPos;
    }

    public void SelectLetter()
    {
        isActivated = !isActivated;

        if (isActivated)
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
                        transform.parent = SelectLetterManager.Instance.cellList[targetCellIndex].transform;

                        JsonFilesController.Instance.allLetters.Remove(gameObject);
                    });
                    break;
                }
            }
        }
        else
        {
            if (targetCellIndex != -1)
            {
                transform.DOMove(letterPos, 0.3f).OnComplete(() =>
                {
                    SelectLetterManager.Instance.cellList[targetCellIndex].isEmpty = false;
                    transform.parent = JsonFilesController.Instance.lettersParent.transform;

                    JsonFilesController.Instance.allLetters.Add(gameObject);

                    targetCellIndex = -1;
                });
            }
        }
    }
}