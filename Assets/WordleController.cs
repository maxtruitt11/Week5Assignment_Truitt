using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleController : MonoBehaviour
{
    public WordleModel model;
    public Text GuessesText;
    public Text GameOverText;

    public void SubmitGuess(string guess)
    {
        if (model.IsValidWord(guess))
        {
            model.SubmitGuess(guess);
          //  UpdateGuessesText();
            CheckForGameOver();
        }
    }

 //   private void UpdateGuessesText()
   // {
 //       GuessesText.text = string.Join("\n", model.guesses);
  //  }

    private void CheckForGameOver()
    {
        if (model.IsGameOver())
        {
            GameOverText.text = "Game Over!";
        }
    }
}