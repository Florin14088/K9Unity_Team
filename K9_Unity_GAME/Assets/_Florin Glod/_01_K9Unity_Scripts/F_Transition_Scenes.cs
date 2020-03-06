using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class F_Transition_Scenes : MonoBehaviour
{
    public string sceneToLoad;


    private bool loadScene = false;
    [SerializeField] private Text loadingText;
    [SerializeField] private Text creator;
    private int counter = 0;

     
    void Update()
    {


        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        {
            counter++;

            if (counter == 1)
            {
                loadingText.text = "LOADING - Have fun and enjoy";
                creator.text = "Game by K9 Unity";
                loadScene = true;

                StartCoroutine(LoadNewScene());
            }

        }


        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

    }


    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(4);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

    }

}//END
