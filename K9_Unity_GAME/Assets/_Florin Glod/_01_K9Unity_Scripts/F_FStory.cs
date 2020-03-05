using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_FStory : MonoBehaviour
{
    #region Own Classes
    [System.Serializable] public class BeCreative
    {
        public string[] Instructions; //texts for the text in canvas
        [HideInInspector] public int index; //the size of the Instructions array
    }

    [System.Serializable] public class FillUp
    {
        public string interestTag;
        [Space]
        public GameObject panel_Instructions; //panel in canvas that is holding the text that is informing player about new messages
        public Text messageInstructions_txt; //text child of the panel above
        public Text whatNext_txt; //here will be player instructed what key to press and what will happen after he presses it
        public KeyCode interaction_key = KeyCode.G; //action key
        public bool b_destroyOnFinish = true;
    }
    #endregion

    #region Public Variables
    [Tooltip("Here is the variable that can be filled with as many strings as needed. Every line is a new message.")]
    public BeCreative beCreative = new BeCreative();
    [Space]
    [Tooltip("Either drag and drop the components here, or fill up the variables")]
    public FillUp fillUp = new FillUp();
    #endregion

    #region Private Variables
    string mirror_interactKey;
    private bool b_inProximity = false;
    private int arraySize = 0; // cache of the size of the array to be used instead of calculating the size of the array every frame
    private F_Game_Manager _script_GM;
    #endregion



    void Start()
    {
        _script_GM = FindObjectOfType<F_Game_Manager>();

        fillUp.panel_Instructions.SetActive(false);
        beCreative.index = 0;
        mirror_interactKey = fillUp.interaction_key.ToString();
        arraySize = beCreative.Instructions.Length;

    }//Start


    private void Update()
    {
        if (b_inProximity && Input.GetKeyDown(fillUp.interaction_key))
        {
            beCreative.index += 1;
        }

        if (b_inProximity)
        {
            if (beCreative.index < beCreative.Instructions.Length)
            {
                fillUp.messageInstructions_txt.text = beCreative.Instructions[beCreative.index];

                if (beCreative.index < arraySize - 1) fillUp.whatNext_txt.text = "Press " + mirror_interactKey + " for the next message";
                if (beCreative.index == arraySize - 1) fillUp.whatNext_txt.text = "This is the last message. Press " + mirror_interactKey + " to close";
            }
            else if (beCreative.index == beCreative.Instructions.Length)
            {
                fillUp.messageInstructions_txt.text = " ";
                fillUp.panel_Instructions.SetActive(false);

                if (fillUp.b_destroyOnFinish)
                {
                    Destroy(gameObject);
                }

            }
        }

    }//Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == fillUp.interestTag)
        {
            fillUp.panel_Instructions.SetActive(true);
            fillUp.messageInstructions_txt.text = beCreative.Instructions[beCreative.index];
            b_inProximity = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == fillUp.interestTag)
        {
            beCreative.index = 0;
            fillUp.messageInstructions_txt.text = " ";
            fillUp.panel_Instructions.SetActive(false);
            b_inProximity = false;
        }
    }
    


}//END
