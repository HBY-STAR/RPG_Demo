using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string settingsSceneName = "SettingsScene";
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        if(SaveManager.Instance.HasSavedData())
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void NewGame()
    {
        SaveManager.Instance.DeleteSavedData();
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        // Application.Quit();
    }

    public void Settings()
    {
        Debug.Log("Settings");
        // SceneManager.LoadScene(gameSceneName);
    }
}
