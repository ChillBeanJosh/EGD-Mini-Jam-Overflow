using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialUI : MonoBehaviour
{
    public Button returnToMenu;


    void Start()
    {
        returnToMenu.onClick.AddListener(StartMenu);
    }

    void StartMenu()
    {
        scenesManager.Instance.LoadMainMenu();
    }
}
