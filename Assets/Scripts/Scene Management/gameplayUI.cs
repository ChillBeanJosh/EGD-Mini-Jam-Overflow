using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameplayUI : MonoBehaviour
{
    public Button play;
    public Button mainMenu;


    void Start()
    {
        play.onClick.AddListener(StartPlay);
        mainMenu.onClick.AddListener(StartMenu);
    }

    void StartPlay()
    {
        scenesManager.Instance.LoadNewGame();
    }

    void StartMenu()
    {
        scenesManager.Instance.LoadMainMenu();
    }
}
