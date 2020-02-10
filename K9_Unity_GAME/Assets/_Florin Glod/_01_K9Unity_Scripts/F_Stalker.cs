using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Stalker : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;


    void Start()
    {
        offset = gameObject.transform.position - target.transform.position;

    }



    void Update()
    {
        gameObject.transform.position = target.transform.position + offset;

    }


}
