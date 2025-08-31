using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button play;
    public Button tutorial;


    void Start()
    {
        play.onClick.AddListener(StartPlay);
        tutorial.onClick.AddListener(StartTutorial);
    }

    void StartPlay()
    {
        scenesManager.Instance.LoadNewGame();
    }

    void StartTutorial()
    {
        scenesManager.Instance.LoadScene(scenesManager.Scene.Tutorial);
    }
}
