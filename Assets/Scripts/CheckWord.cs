using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CheckWord : MonoBehaviour
{
    public static CheckWord Instance;
    
    private string dictionaryPath = "Assets/Dictionary.txt";
    [SerializeField] private List<TMP_Text> completedWordParent;
    [SerializeField] private int lineIndex = 0;
    [SerializeField] private List<string> completedWords;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckCompletedWord()
    {
        string searchableWord = SelectLetterManager.Instance.CombineLetters();
        
        if (File.Exists(dictionaryPath))
        {
            string fileContent = File.ReadAllText(dictionaryPath);
            
            if (fileContent.Contains(searchableWord) && !completedWords.Contains(searchableWord))
            {
                completedWords.Add(searchableWord);
                AddWordToPanel(searchableWord);
                
                SelectLetterManager.Instance.DestroyCells();
                
                //ActionManager.Instance.OnLettersChildrenUpdate?.Invoke();
                
                SelectLetterManager.Instance.ResetLetters();
                
                ActionManager.Instance.OnCellsCreated?.Invoke();
            }
            else
            {
                Debug.Log("string hata");
            }
        }
        else
        {
            Debug.Log("dictionary hata");
        }
    }

    void AddWordToPanel(string word)
    {
        completedWordParent[lineIndex].text = word;
        lineIndex++;
        
        
    }
}
