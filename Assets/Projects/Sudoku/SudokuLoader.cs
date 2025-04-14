using System;
using System.IO;
using UnityEngine;

public class SudokuLoader : MonoBehaviour
{
    public int[] LoadGridFromText(string fileName)
    {

        TextAsset boardText = Resources.Load<TextAsset>(fileName); //file in assets/resource
        if (boardText == null)
        {
            Debug.LogError("file not found: " + fileName);
            return null;
        }

        string rawText = boardText.text
        .Replace("\n", "")
        .Replace("\r", "")
        .Replace(" ", ""); // remove all whitespace just in case

        if (rawText.Length != 81)
        {
            Debug.LogError("Invalid board data. Must contain exactly 81 digits. Currently contains: " + rawText.Length);
            return null;
        }

        int[] boardValues = new int[81];

        for (int i = 0; i < 81; i++)
        {
            if (!int.TryParse(rawText[i].ToString(), out boardValues[i]))
            {
                Debug.LogError($"Invalid character at index {i}: '{rawText[i]}'");
                return null;
            }
        }
    
        return boardValues;

    }
}
