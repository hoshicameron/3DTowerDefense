using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionMenuUI;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Scene_Game");
    }

    public void ShowOptions()
    {
        mainMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void HideOptions()
    {
        mainMenuUI.SetActive(true);
        optionMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        print("Application Closed...");
        Application.Quit();
    }

    public void RestartGame()
    {

    }
}
