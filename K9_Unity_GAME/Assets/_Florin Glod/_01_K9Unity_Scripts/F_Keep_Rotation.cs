using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Keep_Rotation : MonoBehaviour
{
    public float initialRotation = 100;

    void Start()
    {

    }


    void LateUpdate()
    {
        if (gameObject.transform.rotation.y != initialRotation)
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, initialRotation, gameObject.transform.rotation.z);

    }//LateUpdate

}//END
