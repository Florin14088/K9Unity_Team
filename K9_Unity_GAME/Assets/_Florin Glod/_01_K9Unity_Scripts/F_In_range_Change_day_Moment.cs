using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_In_range_Change_day_Moment : MonoBehaviour
{
    public GameObject panel_notice_sleep;
    public GameObject panel_momentsOfTheDay;
    public KeyCode interactKey = KeyCode.F;

    private F_Game_Manager _script_GM;
    private bool b_inRange = false;


    private void Start()
    {
        _script_GM = FindObjectOfType<F_Game_Manager>();
        panel_notice_sleep.SetActive(false);
    }

    private void Update()
    {
        if (b_inRange && Input.GetKeyDown(interactKey))
        {
            panel_momentsOfTheDay.SetActive(true);
            _script_GM.StopTime();
        }


    }//Update

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.root.tag == "Player")
        {
            b_inRange = true;
            panel_notice_sleep.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.tag == "Player")
        {
            b_inRange = false;
            panel_notice_sleep.SetActive(false);
        }
    }

}
