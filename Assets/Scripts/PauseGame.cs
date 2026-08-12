using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] Button unpauseButton;
    [SerializeField] Button goToMenuButton;
    [SerializeField] Button QuitGameButton;
    [SerializeField] GameObject pausePanel;


    [SerializeField] InputAction pauseGame;

    private void Start()
    {
        pauseGame = InputSystem.actions.FindAction("Pause");

        pauseGame.performed += PauseGame_performed;
    }

    private void PauseGame_performed(InputAction.CallbackContext obj)
    {
         pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }



    public void Unpause()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
