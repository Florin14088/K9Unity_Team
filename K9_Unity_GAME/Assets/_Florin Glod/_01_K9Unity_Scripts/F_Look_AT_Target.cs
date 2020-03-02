using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Look_AT_Target : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.LookAt(target);
    }

}
