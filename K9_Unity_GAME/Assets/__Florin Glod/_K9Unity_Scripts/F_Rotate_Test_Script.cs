using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Rotate_Test_Script : MonoBehaviour
{
    public float xA, yA, zA;

    void Start()
    {
        
    }


    void Update()
    {
        gameObject.transform.Rotate(xA, yA, zA, Space.Self);
    }
}
