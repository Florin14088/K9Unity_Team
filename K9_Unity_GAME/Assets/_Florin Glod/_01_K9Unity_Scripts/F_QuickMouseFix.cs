using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_QuickMouseFix : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
