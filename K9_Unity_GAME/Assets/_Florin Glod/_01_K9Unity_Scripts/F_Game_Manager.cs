using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class F_Game_Manager : MonoBehaviour
{
    public GameObject InfoPanel;
    public string tutorial_sceneName;
    public string level_sceneName;
    public string credits_sceneName;
    public string mainMenu_sceneName;



    void Start()
    {

    }


    void Update()
    {
        

    }


    #region Button
    public void ButtonCall_Info()
    {
        InfoPanel.SetActive(true);
        StopTime();

    }//ButtonCall_Info


    public void StopTime()
    {
        Time.timeScale = 0;
    }


    public void StartTime()
    {
        Time.timeScale = 1;
    }


    public void ButtonCall_ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ButtonCall_Load_MainMenu()
    {
        SceneManager.LoadScene(mainMenu_sceneName);
    }


    public void ButtonCall_Load_Credits()
    {
        SceneManager.LoadScene(credits_sceneName);
    }


    public void ButtonCall_Load_Tutorial()
    {
        SceneManager.LoadScene(tutorial_sceneName);
    }


    public void ButtonCall_Load_Level()
    {
        SceneManager.LoadScene(level_sceneName);
    }


    public void ButtonCall_ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

#endregion

}//END
