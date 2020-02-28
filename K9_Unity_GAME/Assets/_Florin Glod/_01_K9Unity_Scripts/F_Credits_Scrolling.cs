using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class F_Credits_Scrolling : MonoBehaviour
{
    public bool b_enabled = false;
    public bool b_restartable = false;
    [Range(0.0001f, 0.001f)] public float changeValue = 0;
    [HideInInspector] public float nextChange = 0;
    public Scrollbar bar_scrolling;




    void Update()
    {
        if (b_enabled)
        {
            if (bar_scrolling.value > 0)
            {
                if (Time.time > nextChange)
                {
                    nextChange = Time.time + changeValue;
                    bar_scrolling.value -= changeValue;

                }
            }

            if (bar_scrolling.value <= 0)
            {
                b_enabled = false;
                bar_scrolling.value = 0;
            }

        }//enabled


        if (b_restartable)
        {
            if (bar_scrolling.value < 1)
            {
                if (Time.time > nextChange)
                {
                    nextChange = Time.time + changeValue;
                    bar_scrolling.value += 6 * changeValue;

                }
            }

            if(bar_scrolling.value > 1)
            {
                b_restartable = false;
                bar_scrolling.value = 1;
            }

        }//restartable
        


    }//Update



    public void Change_EnabledBool()
    {
        b_enabled = !b_enabled;

    }//Change_EnabledBool


    public void Change_RestartableBool()
    {
        b_restartable = !b_restartable;

    }//Change_RestartableBool



}//END
