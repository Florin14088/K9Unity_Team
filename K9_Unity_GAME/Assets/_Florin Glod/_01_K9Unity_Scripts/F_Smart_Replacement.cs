using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class F_Smart_Replacement : MonoBehaviour
{
    public GameObject prefab_nextGen;
    [Space]
    public string tag_summer_Objects = "oldThings";
    public string tag_winter_Objects = "newThings";
    public GameObject[] summerTrees;
    public GameObject[] winterTrees;
    [Space]
    [Space]
    public bool b_search_summerTree = false;
    public bool b_search_winterTree = false;
    [Space]
    public bool b_Place_WinterTree = false;
    public bool b_Place_SummerTree = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (b_search_summerTree)
        {
            b_search_summerTree = false;
            summerTrees = GameObject.FindGameObjectsWithTag(tag_summer_Objects);
        }


        if (b_search_winterTree)
        {
            b_search_winterTree = false;
            winterTrees = GameObject.FindGameObjectsWithTag(tag_winter_Objects);
        }


        if (b_Place_WinterTree)
        {
            b_Place_WinterTree = false;

            foreach (GameObject g in summerTrees)
            {
                Instantiate(prefab_nextGen, g.transform.position, g.transform.rotation);
                g.SetActive(false);
            }

            return;
        }


        if (b_Place_SummerTree)
        {
            b_Place_SummerTree = false;

            foreach (GameObject g in winterTrees) g.SetActive(false);
            foreach (GameObject g in summerTrees) g.SetActive(true);

            return;
        }


    }//Update




}
