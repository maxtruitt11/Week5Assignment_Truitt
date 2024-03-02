using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordleModel : MonoBehaviour
{
    public GameObject canvas;
    public List<string> wordleBoard;
    public List<string> wordList;
    public string currentWord;
    public int attemptsLeft;
    public Text[] cells;

    public WordleModel()
    {
        wordleBoard = new List<string>();
        wordList = new List<string> { "APPLE", "BANANA", "CHERRY", "DATE", "FIG", "GRAPE", "KIWI" };
        currentWord = "";
        attemptsLeft = 6;

        for (int i = 0; i < 6; i++)
        {
            wordleBoard.Add(new string('_', 5));
        }
    }

    public List<string> WordleBoard
    {
        get { return wordleBoard; }
        private set { wordleBoard = value; }
    }

    public void Start()
    {
        gridLayoutGroup = canvas.GetComponentInChildren<GridLayoutGroup>();
        cells = new Text[gridLayoutGroup.transform.childCount];

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = gridLayoutGroup.transform.GetChild(i).GetComponent<Text>();
        }
    }

    public void SubmitGuess(string word)
    {
        if (IsValidWord(word))
        {
            currentWord = word.ToUpper();
            int correctGuessCount = 0;

            for (int i = 0; i < 5; i++)
            {
                if (currentWord[i] == wordList[0][i])
                {
                    wordleBoard[wordleBoard.Count - attemptsLeft + i] = currentWord[i].ToString();
                    correctGuessCount++;
                }
                else
                {
                    bool containsLetter = false;
                    for (int j = 0; j < 5; j++)
                    {
                        if (currentWord[j] == wordList[0][i])
                        {
                            containsLetter = true;
                            break;
                        }
                    }

                    if (containsLetter)
                    {
                        wordleBoard[i] = word[i].ToString();
                    }
                }
            }

            attemptsLeft -= Math.Max(0, correctGuessCount - 1);

            // Update the cells to display the user's guess
            for (int i = 0; i < 5; i++)
            {
                cells[i].text = wordleBoard[attemptsLeft + i];
            }

            // Disable any remaining cells for the current word
            for (int i = 5; i < cells.Length; i++)
            {
                cells[i].text = "";
                cells[i].enabled = false;
            }

            // Enable all cells for the next word
            for (int i = 5; i < cells.Length; i++)
            {
                cells[i].enabled = true;
            }
        }
        else
        {
            throw new ArgumentException("The submitted word is not in the word list.");
        }
    }

    public bool IsValidWord(string word)
    {
        return wordList.Contains(word.ToUpper());
    }

    public string GetWordleBoard()
    {
        string wordleBoardString = "";

        foreach (var line in wordleBoard)
        {
            wordleBoardString += line + "\n";
        }

        return wordleBoardString;
    }

    public bool IsGameOver()
    {
        return attemptsLeft == 0;
    }

    public void RestartGame()
    {
        wordleBoard = new List<string>();
        currentWord = "";
        attemptsLeft = 6;

        for (int i = 0; i < 6; i++)
        {
            wordleBoard.Add(new string('_', 5));
        }
    }
}