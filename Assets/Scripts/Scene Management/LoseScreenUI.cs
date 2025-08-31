using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenUI : MonoBehaviour
{
    public Button playAgain;
    public Button mainMenu;


    void Start()
    {
        playAgain.onClick.AddListener(StartPlay);
        mainMenu.onClick.AddListener(StartMainMenu);
    }

    void StartPlay()
    {
        scenesManager.Instance.LoadNewGame();
    }

    void StartMainMenu()
    {
        scenesManager.Instance.LoadMainMenu();
    }
}
