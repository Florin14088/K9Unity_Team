using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class F_Game_Manager : MonoBehaviour
{
    #region Own Classes
    [System.Serializable] public class byFlorin__forScenes
    {
        public string tutorial_sceneName;
        public string level_sceneName;
        public string credits_sceneName;
        public string mainMenu_sceneName;
    }

    [System.Serializable] public class byFlorin__Pickup
    {
        public int collectedAmount = 0;
        [Space]
        public bool b_allowPanelShowing = false;
        public GameObject ui_panel_pickup_notice;
        [Range(1, 4)] public float time_alive_panel = 3;        
    }

    [System.Serializable] public class byFlorin__UI
    {
        public Text collected_txt;
        [Space]
        public Button enter_menuButton;
    }
    #endregion


    #region Public
    public byFlorin__forScenes florin_scenes = new byFlorin__forScenes();
    [Space]
    public byFlorin__Pickup florin_pickup = new byFlorin__Pickup();
    [Space]
    public byFlorin__UI florin_UI = new byFlorin__UI();
    #endregion


    #region Private
    private float _tempAvailableTimer = 0;
    private float cooldown = 1;
    private float nextCooldown = 0;
    #endregion


    #region Pre Defined Functions
    void Start()
    {
        if(florin_UI.collected_txt) florin_UI.collected_txt.text = "0";

    }//Start


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(florin_UI.enter_menuButton) florin_UI.enter_menuButton.onClick.Invoke();
        }

        ScoreIsChanged();
        GoingToDisablePanel();

    }//Update
    #endregion


    #region Own Functions

    private void ScoreIsChanged()
    {
        if (florin_pickup.b_allowPanelShowing)
        {
            florin_pickup.b_allowPanelShowing = false;
            florin_UI.collected_txt.text = florin_pickup.collectedAmount.ToString("f0");
            _tempAvailableTimer += florin_pickup.time_alive_panel;
            florin_pickup.ui_panel_pickup_notice.SetActive(true);
        }

    }//ScoreIsChanged

    private void GoingToDisablePanel()
    {
        if(_tempAvailableTimer > 0)
        {
            if(Time.time > nextCooldown)
            {
                nextCooldown = Time.time + cooldown;
                _tempAvailableTimer--;
            }
        }

        if(_tempAvailableTimer <= 0 && florin_pickup.ui_panel_pickup_notice.activeSelf)
        {
            florin_pickup.ui_panel_pickup_notice.SetActive(false);
        }

    }//GoingToDisablePanel

    #endregion


    #region Button
    
    public void StopTime()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }


    public void StartTime()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }


    public void ButtonCall_ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ButtonCall_Load_MainMenu()
    {
        SceneManager.LoadScene(florin_scenes.mainMenu_sceneName);
    }


    public void ButtonCall_Load_Credits()
    {
        SceneManager.LoadScene(florin_scenes.credits_sceneName);
    }


    public void ButtonCall_Load_Tutorial()
    {
        SceneManager.LoadScene(florin_scenes.tutorial_sceneName);
    }


    public void ButtonCall_Load_Level()
    {
        SceneManager.LoadScene(florin_scenes.level_sceneName);
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
