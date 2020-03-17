using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Destroy : MonoBehaviour
{
    public float secondsDestry = 5;

    void Start()
    {
        Destroy(gameObject, secondsDestry);
    }

}
