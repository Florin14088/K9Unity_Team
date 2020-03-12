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
        [Space]
        public string transition_toTutorial_sceneName;
        public string transition_toGame_sceneName;
    }

    [System.Serializable] public class byFlorin__Pickup
    {
        public int collectedAmount = 0;
        public bool b_allowPanelShowing = false;
        public GameObject ui_panel_pickup_notice;
        [Range(1, 4)] public float time_alive_panel = 3;  
        [Space]
        public int FOXCollectedAmount = 0;
        public bool b_FOX_allowPanelShowing = false;
        public GameObject ui_panel_FOX_pickup_notice;
        [Range(1, 4)] public float time_alive_panel_FOX = 3;
    }

    [System.Serializable] public class byFlorin__UI
    {
        public Text collected_txt;
        [Space]
        public Text FOX_collected_txt;
        [Space]
        public Button enter_menuButton;
        [Space]
        public GameObject ToDenPanel;
    }

    [System.Serializable] public class byFlorin_Creativity
    {
        public string[] messages_pickUp_Food;
        [Space]
        public string[] messages_find_Fox;
    }
    #endregion


    #region Public
    public byFlorin__forScenes florin_scenes = new byFlorin__forScenes();
    [Space]
    public byFlorin__Pickup florin_pickup = new byFlorin__Pickup();
    [Space]
    public byFlorin__UI florin_UI = new byFlorin__UI();
    [Space]
    public byFlorin_Creativity florin_creative = new byFlorin_Creativity();
    [Space]
    [Space]
    public int foodRequired = 20;
    public int foxesRequired = 3;
    public GameObject portalToCredits;
    #endregion


    #region Private
    private float _tempAvailableTimer = 0;
    private float _FOXtempAvailableTimer = 0;
    private float cooldown = 1;
    private float FOXcooldown = 1;
    private float nextCooldown = 0;
    private float FOXnextCooldown = 0;
    #endregion


    #region Pre Defined Functions
    void Start()
    {
        if(florin_UI.collected_txt) florin_UI.collected_txt.text = "0";
        if(florin_UI.FOX_collected_txt) florin_UI.FOX_collected_txt.text = "0";
        if (portalToCredits) portalToCredits.SetActive(false);

    }//Start


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(florin_UI.enter_menuButton) florin_UI.enter_menuButton.onClick.Invoke();
        }

        ScoreIsChanged();
        GoingToDisablePanel();
        FOX_ScoreIsChanged();
        FOX_GoingToDisablePanel();

        if (foodRequired <= 0 && foxesRequired <= 0)
        {
            if (portalToCredits.activeSelf == false) portalToCredits.SetActive(true);
            if (florin_UI.ToDenPanel.activeSelf == false) florin_UI.ToDenPanel.SetActive(true);
        }

    }//Update
    #endregion


    #region Own Functions

    private void ScoreIsChanged()
    {
        if (florin_pickup.b_allowPanelShowing)
        {
            florin_pickup.b_allowPanelShowing = false;
            florin_UI.collected_txt.text = florin_pickup.collectedAmount.ToString("f0");
            _tempAvailableTimer = florin_pickup.time_alive_panel;
            florin_pickup.ui_panel_pickup_notice.SetActive(true);
            florin_pickup.ui_panel_pickup_notice.GetComponentInChildren<Text>().text = florin_creative.messages_pickUp_Food[Random.Range(0, florin_creative.messages_pickUp_Food.Length)];
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

        if(_tempAvailableTimer <= 0 && florin_pickup.ui_panel_pickup_notice && florin_pickup.ui_panel_pickup_notice.activeSelf)
        {
            florin_pickup.ui_panel_pickup_notice.SetActive(false);
        }

    }//GoingToDisablePanel


    private void FOX_ScoreIsChanged()
    {
        if (florin_pickup.b_FOX_allowPanelShowing)
        {
            florin_pickup.b_FOX_allowPanelShowing = false;
            florin_UI.FOX_collected_txt.text = florin_pickup.FOXCollectedAmount.ToString("f0");
            _FOXtempAvailableTimer = florin_pickup.time_alive_panel_FOX;
            florin_pickup.ui_panel_FOX_pickup_notice.SetActive(true);
            florin_pickup.ui_panel_FOX_pickup_notice.GetComponentInChildren<Text>().text = florin_creative.messages_find_Fox[Random.Range(0, florin_creative.messages_find_Fox.Length)];
        }

    }//FOX_ScoreIsChanged

    private void FOX_GoingToDisablePanel()
    {
        if (_FOXtempAvailableTimer > 0)
        {
            if (Time.time > FOXnextCooldown)
            {
                FOXnextCooldown = Time.time + FOXcooldown;
                _FOXtempAvailableTimer--;
            }
        }

        if (_FOXtempAvailableTimer <= 0 && florin_pickup.ui_panel_FOX_pickup_notice && florin_pickup.ui_panel_FOX_pickup_notice.activeSelf)
        {
            florin_pickup.ui_panel_FOX_pickup_notice.SetActive(false);
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


    public void StartTime_CursorVisible()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void ButtonCall_Load_Transition_Menu_Tutorial()
    {
        SceneManager.LoadScene(florin_scenes.transition_toTutorial_sceneName);
    }

    public void ButtonCall_Load_Transition_Menu_Game()
    {
        SceneManager.LoadScene(florin_scenes.transition_toGame_sceneName);
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
