using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesManager : MonoBehaviour
{
    public static scenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public enum Scene
    {
        MainMenu,
        GamePlay,
        WinScreen,
        LoseScreen,
        Tutorial,
        test
    }



    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }


    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.GamePlay.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
