using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class F_Random_Y_Rotate : MonoBehaviour
{

    void Start()
    {
        gameObject.transform.root.rotation = Quaternion.Euler(gameObject.transform.root.rotation.x, Random.Range(0, 300), gameObject.transform.root.rotation.z);
        F_Random_Y_Rotate cra = gameObject.GetComponent<F_Random_Y_Rotate>();
        DestroyImmediate(cra);

    }

}
