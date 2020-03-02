using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Stalker : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;
    [Space]
    public bool b_allowForceReset = false;
    public KeyCode resetKey = KeyCode.R;


    void Start()
    {
        offset = gameObject.transform.position - target.transform.position;

    }



    void Update()
    {
        gameObject.transform.position = target.transform.position + offset;

        if(Input.GetKeyDown(resetKey) && b_allowForceReset)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0, 0, 0), 360);
        }

    }


}
