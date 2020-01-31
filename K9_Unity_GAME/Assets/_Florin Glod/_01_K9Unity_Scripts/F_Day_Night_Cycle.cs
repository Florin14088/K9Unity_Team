#region using Owner.Florin
//Script created by Florin Glod on January 31, 2020;
//This script cannot be used by any person, entity, or organization without written permission from the creator.
//All rights reserved.
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Day_Night_Cycle : MonoBehaviour
{
    #region Own Classes



    #endregion


    #region Public Variables

    public GameObject Sun;
    [Space]
    public bool b_enabledCycle = false;
    public float cooldownTempo = 0.3f;
    [HideInInspector] public float nextTempo = 0;
    [Space]
    public float unitsPerTempo = 5;

    #endregion


    #region Private Variables



    #endregion


    #region Pre-defined functions

    private void Awake()
    {


    }//Awake


    void Start()
    {


    }//Start


    void Update()
    {
        if (b_enabledCycle)
        {
            if(Time.time > nextTempo)
            {
                nextTempo = Time.time + cooldownTempo;
                Sun.transform.Rotate(unitsPerTempo, Sun.transform.rotation.y, Sun.transform.rotation.z, Space.Self);
            }
        }

    }//Update

    #endregion


    #region Own Functions



    #endregion


    #region Functions that return something



    #endregion

}//END
