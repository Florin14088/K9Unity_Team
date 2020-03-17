using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class F_Enable_In_Order : MonoBehaviour
{
    public GameObject[] all_fish;
    public float cooldown = 1.5f;
    [HideInInspector] public float nextCooldown = 0;
    public int index = 0;
    public int total_count = 0;

    void Start()
    {
        //all_fish = GameObject.FindGameObjectsWithTag("animal_fish");
        total_count = all_fish.Length;
        index = 0;
        //Debug.Log(index);
       // Debug.Log(total_count);
        foreach (GameObject g in all_fish) g.SetActive(false);
    }


    void Update()
    {
        if (Time.time > nextCooldown)
        {
            nextCooldown = Time.time + cooldown;

            if (index < total_count)
            {
                all_fish[index].SetActive(true);
                index++;
            }

            if(index >= total_count)
            gameObject.GetComponent<F_Enable_In_Order>().enabled = false;


        }
    }//Update

}//END
