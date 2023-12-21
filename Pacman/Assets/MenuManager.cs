using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text winText, loseText;
    public Pacman pacman;

    private void Start()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (FindObjectOfType<GameManager>()._init)
        {
            switch(pacman.win)
            {
                case null:
                break;
                case true: 
                    if (winText.gameObject.activeSelf == false) 
                    {
                        DisplayGameWin(); 
                        Debug.Log("set");
                    }
                break;
                case false: if (loseText.gameObject.activeSelf == false) DisplayGameEnd(); Debug.Log("set");
                break;
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseAI()
    {
        var ghosts = FindObjectsOfType<GhostController>();
        foreach (GhostController ghost in ghosts)
        {
            ghost.ToggleDisable();
        }
    }

    public void End()
    {
        Application.Quit();
    }

    public void DisplayGameWin()
    {
        winText.gameObject.SetActive(true);
    }

    public void DisplayGameEnd()
    {
        loseText.gameObject.SetActive(true);
    }
}
