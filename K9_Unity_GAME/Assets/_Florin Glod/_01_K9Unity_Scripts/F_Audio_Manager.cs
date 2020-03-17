using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Audio_Manager : MonoBehaviour
{
    public GameObject night_bird_sound;
    public GameObject day_bird_sound;
    public GameObject collect_sound;
    [Space]
    public F_Day_Night_Cycle _script_DayNight;


    private bool b_onceNight = false;
    private float cooldown = 20;
    private float nextCooldown = 0;


    void Start()
    {
        _script_DayNight = FindObjectOfType<F_Day_Night_Cycle>();

    }//Start


    private void Update()
    {
        if (Time.time > nextCooldown)
        {
            nextCooldown = Time.time + cooldown;

            if (_script_DayNight.day.current_day_moment >= 0.2f && _script_DayNight.day.current_day_moment <= 0.73)
            {
                DayBirds();
            }
            else
            {
                NightBirds();
            }

        }
        
    }//Update


    public void NightBirds()
    {
        GameObject g = Instantiate(night_bird_sound, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void DayBirds()
    {
        GameObject g = Instantiate(day_bird_sound, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void CollectedBerry()
    {
        GameObject g = Instantiate(collect_sound, gameObject.transform.position, gameObject.transform.rotation);
    }


}//END
