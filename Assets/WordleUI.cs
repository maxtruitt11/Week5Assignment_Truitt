using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordleUI : MonoBehaviour
{
    public WordleModel model;
    public TMP_InputField inputField;
    public TMP_Text guessesText;
    public TMP_Text gameOverText;

    private void Start()
    {
        inputField.onEndEdit.AddListener(SubmitGuess);
        gameOverText.gameObject.SetActive(false);
    }

    private void SubmitGuess(string guess)
    {
        if (guess.Length == 5 && model.IsValidWord(guess))
        {
            model.SubmitGuess(guess);
            UpdateGuessesText();
            CheckForGameOver();
        }
    }

    private void UpdateGuessesText()
    {
        guessesText.text = string.Join("\n", model.wordleBoard);
    }

    private void CheckForGameOver()
    {
        if (model.IsGameOver())
        {
            gameOverText.text = "Game Over!";
            gameOverText.gameObject.SetActive(true);
        }
    }
}