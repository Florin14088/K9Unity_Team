using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Pick_Up : MonoBehaviour
{
    #region Own classes
    [System.Serializable] public class MadeByFlorin
    {
        public string[] interestTags;
    }

    [System.Serializable] public class AlsoMadeByFlorin
    {
        public GameObject player_itself;
    }
    #endregion

    #region Public
    public MadeByFlorin florin = new MadeByFlorin();
    public AlsoMadeByFlorin also_florin = new AlsoMadeByFlorin();
    #endregion

    #region Private    
    private F_Game_Manager _script_GM;
    #endregion



    void Start()
    {
        _script_GM = FindObjectOfType<F_Game_Manager>();

        also_florin.player_itself = GameObject.FindGameObjectWithTag("Player");
        
    }//Start


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == also_florin.player_itself)
        {
            _script_GM.florin_pickup.collectedAmount++;
            _script_GM.florin_pickup.b_allowPanelShowing = true;
            Destroy(gameObject);
        }
    }


}//END

