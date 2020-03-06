using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Pick_Up : MonoBehaviour
{
    #region Own classes
    [System.Serializable] public class MadeByFlorin
    {
        public bool b_canBeUsed = true;
        public string interestTag;
        public KeyCode keyCollecting = KeyCode.C;
    }

    [System.Serializable] public class AlsoMadeByFlorin
    {
        public GameObject player_itself;
        public GameObject willBeDestroyed;
        public GameObject PanelUI_InfoCanPick;
    }
    #endregion

    #region Public
    public MadeByFlorin florin = new MadeByFlorin();
    public AlsoMadeByFlorin also_florin = new AlsoMadeByFlorin();
    #endregion

    #region Private    
    private F_Game_Manager _script_GM;
    private bool b_inRadius = false;
    #endregion



    void Start()
    {
        also_florin.PanelUI_InfoCanPick.SetActive(false);
        _script_GM = FindObjectOfType<F_Game_Manager>();

        also_florin.player_itself = GameObject.FindGameObjectWithTag(florin.interestTag);
        
    }//Start


    private void Update()
    {
        if (florin.b_canBeUsed)
        {
            if (b_inRadius && Input.GetKeyDown(florin.keyCollecting))
            {
                TriggerBehaviour();
            }
        }
        

    }//Update


    private void TriggerBehaviour()
    {
        GameObject.FindObjectOfType<F_Game_Manager>().foodRequired--;
        florin.b_canBeUsed = false;
        _script_GM.florin_pickup.collectedAmount++;
        _script_GM.florin_pickup.b_allowPanelShowing = true;
        also_florin.PanelUI_InfoCanPick.SetActive(false);
        Destroy(also_florin.willBeDestroyed);

    }//TriggerBehaviour


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.root.tag == florin.interestTag && florin.b_canBeUsed)
        {
            b_inRadius = true;
            also_florin.PanelUI_InfoCanPick.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.tag == florin.interestTag)
        {
            b_inRadius = false;
            also_florin.PanelUI_InfoCanPick.SetActive(false);
        }
    }


}//END

