using System;
using UnityEngine;
using UnityEngine.UI;

public class WordleView : MonoBehaviour
{
    private WordleModel wordleModel;

    public event Action<string> OnSubmit;
    public event Action OnRestart;

    public Text wordleBoard;
    public InputField inputField;
    public Button submitButton;
    public Button restartButton;
    public Text gameOverMessage;
    public Text invalidWordMessage;

    public WordleView(WordleModel wordleModel)
    {
        this.wordleModel = wordleModel;

        // Subscribe to the OnSubmit and OnRestart events
        OnSubmit += HandleSubmit;
        OnRestart += HandleRestart;

        // Set up the UI elements
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    public void UpdateView()
    {
        // Update the wordle board UI based on the current state of the game
        wordleBoard.text = wordleModel.GetWordleBoard();
    }

    public void ShowGameOverMessage()
    {
        // Display a message indicating that the game is over
        gameOverMessage.gameObject.SetActive(true);
    }

    public void ShowInvalidWordMessage()
    {
        // Display a message indicating that the submitted word is invalid
        invalidWordMessage.gameObject.SetActive(true);
    }

    public void OnSubmitButtonClicked()
    {
        // Get the current input from the user
        string word = inputField.text;

        // Call the OnSubmit event with the current input
        OnSubmit?.Invoke(word);
    }

    public void OnRestartButtonClicked()
    {
        // Call the OnRestart event
        OnRestart?.Invoke();
    }

    private void HandleSubmit(string word)
    {
        // Update the game state based on the submitted word
        wordleModel.SubmitGuess(word);

        // Update the view to reflect the new game state
        UpdateView();

        // Check if the game is over
        if (wordleModel.IsGameOver())
        {
            ShowGameOverMessage();
        }
        else
        {
            // Clear the input field for the next guess
            inputField.text = "";
        }
    }

    private void HandleRestart()
    {
        // Reset the game state
        wordleModel.RestartGame();

        // Update the view to reflect the new game state
        UpdateView();

        // Hide the game over message
        gameOverMessage.gameObject.SetActive(false);

        // Clear the input field for the next round
        inputField.text = "";
    }
}