using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Auto_Placement : MonoBehaviour
{
    


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        Destroy(r);
        BoxCollider c = GetComponent<BoxCollider>();
        Destroy(c);
        F_Auto_Placement f = GetComponent<F_Auto_Placement>();
        Destroy(f);
    }
}
