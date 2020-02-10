#region using Owner.Florin Glod
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
    [System.Serializable] public class Master
    {
        public Light sun;
        public Light PlayerLight;
        public GameObject SkyStars;
        public Transform[] allStars;
        [HideInInspector] public enum DayMoments_Teleport {midnight, dusk, morning, day, dawn, night};
        [Space]
        public DayMoments_Teleport pickedMoment;
    }

    [System.Serializable] public class DAY
    {
        public bool b_auto_dayPass = false;
        [Space]
        public float seconds_for_one_day = 120f;
        [Space]
        [Range(0, 1)] public float current_day_moment = 0;
        [HideInInspector] public float timeMultiplier = 1f;
        [Space]
        public Color starColor_inviz;
        public Color starColor_viz;
    }
    #endregion


    #region Public
    public Master master = new Master();
    [Space]
    public DAY day = new DAY();
    #endregion


    #region Private
    private float sunInitialIntensity;
    #endregion




    void Start()
    {
        master.allStars = master.SkyStars.GetComponentsInChildren<Transform>();

        sunInitialIntensity = master.sun.intensity;
        Make_Day();
        ChangeDayStage();

    }//Start


    void Update()
    {
        if(day.b_auto_dayPass) UpdateSun();

        if (day.current_day_moment >= 1) day.current_day_moment = 0;

    }//Update


    void UpdateSun()
    {
        day.current_day_moment += (Time.deltaTime / day.seconds_for_one_day) * day.timeMultiplier;
        master.sun.transform.localRotation = Quaternion.Euler((day.current_day_moment * 360f) - 90, 170, 0);

        master.SkyStars.transform.localRotation = Quaternion.Euler((day.current_day_moment * 360f) - 90, 170, 0);


        float intensityMultiplier = 1;

        if (day.current_day_moment <= 0.23f || day.current_day_moment >= 0.75f)
        {
            intensityMultiplier = 0;

        }
        else if (day.current_day_moment <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((day.current_day_moment - 0.23f) * (1 / 0.02f));

        }
        else if (day.current_day_moment >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((day.current_day_moment - 0.73f) * (1 / 0.02f)));

        }

        if (day.current_day_moment > 0.22f && day.current_day_moment < 0.72f)
        {
            foreach (Transform t in master.allStars) if (t.GetComponent<MeshRenderer>()) t.GetComponent<MeshRenderer>().material.color = Color.Lerp(t.GetComponent<MeshRenderer>().material.color, day.starColor_inviz, day.seconds_for_one_day / 5 * Time.deltaTime);

        }
        else if(day.current_day_moment < 0.22f || day.current_day_moment > 0.72f)
        {
            foreach (Transform t in master.allStars) if (t.GetComponent<MeshRenderer>()) t.GetComponent<MeshRenderer>().material.color = Color.Lerp(t.GetComponent<MeshRenderer>().material.color, day.starColor_viz, day.seconds_for_one_day / 5 * Time.deltaTime);

        }

        master.sun.intensity = sunInitialIntensity * intensityMultiplier;
        master.PlayerLight.intensity = 1 - master.sun.intensity;

    }//UpdateSun


    #region Button management
    public void ChangeDayStage()
    {   //..........midnight.........dusk..........morning.........day............dawn......night......
        //............//0//........//0.2//.........//0.3//.......//0.5//........//0.75//...//0.9//.....
        switch (master.pickedMoment)
        {
            case Master.DayMoments_Teleport.midnight:
                day.current_day_moment = 0;
                break;

            case Master.DayMoments_Teleport.dusk:
                day.current_day_moment = 0.2f;
                break;

            case Master.DayMoments_Teleport.morning:
                day.current_day_moment = 0.3f;
                break;

            case Master.DayMoments_Teleport.day:
                day.current_day_moment = 0.5f;
                break;

            case Master.DayMoments_Teleport.dawn:
                day.current_day_moment = 0.75f;
                break;

            case Master.DayMoments_Teleport.night:
                day.current_day_moment = 0.9f;
                break;

            default:
                Debug.LogError("How did you get here?");
                break;

        }//switch

        UpdateSun();

    }//ChangeDayStage


    public void Make_Midnight()
    {
        master.pickedMoment = Master.DayMoments_Teleport.midnight;
    }

    public void Make_Dusk()
    {
        master.pickedMoment = Master.DayMoments_Teleport.dusk;
    }

    public void Make_Morning()
    {
        master.pickedMoment = Master.DayMoments_Teleport.morning;
    }

    public void Make_Day()
    {
        master.pickedMoment = Master.DayMoments_Teleport.day;
    }

    public void Make_Dawn()
    {
        master.pickedMoment = Master.DayMoments_Teleport.dawn;
    }

    public void Make_Night()
    {
        master.pickedMoment = Master.DayMoments_Teleport.night;
    }

    #endregion


}//END


//#region using Owner.Florin Glod
////Script created by Florin Glod on January 31, 2020;
////This script cannot be used by any person, entity, or organization without written permission from the creator.
////All rights reserved.
//#endregion
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class F_Day_Night_Cycle : MonoBehaviour
//{
//    public Light sun;
//    public Light PlayerLight;
//    [Space]
//    public float seconds_for_one_day = 120f;
//    [Range(0, 1)]
//    public float current_day_moment = 0;
//    [HideInInspector]
//    public float timeMultiplier = 1f;

//    float sunInitialIntensity;

//    void Start()
//    {
//        sunInitialIntensity = sun.intensity;
//    }

//    void Update()
//    {
//        UpdateSun();

//        current_day_moment += (Time.deltaTime / seconds_for_one_day) * timeMultiplier;

//        if (current_day_moment >= 1)
//        {
//            current_day_moment = 0;
//        }
//    }

//    void UpdateSun()
//    {
//        sun.transform.localRotation = Quaternion.Euler((current_day_moment * 360f) - 90, 170, 0);

//        float intensityMultiplier = 1;
//        if (current_day_moment <= 0.23f || current_day_moment >= 0.75f)
//        {
//            intensityMultiplier = 0;
//        }
//        else if (current_day_moment <= 0.25f)
//        {
//            intensityMultiplier = Mathf.Clamp01((current_day_moment - 0.23f) * (1 / 0.02f));
//        }
//        else if (current_day_moment >= 0.73f)
//        {
//            intensityMultiplier = Mathf.Clamp01(1 - ((current_day_moment - 0.73f) * (1 / 0.02f)));
//        }

//        sun.intensity = sunInitialIntensity * intensityMultiplier;
//        PlayerLight.intensity = 1 - sun.intensity;

//    }

//}//END