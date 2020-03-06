using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class F_Load_Scene : MonoBehaviour
{
    public string sceneName;
    public bool b_enableMouseControl = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.root.tag == "Player")
        {
            if (b_enableMouseControl)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }


}
