using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class F_Simple_Follow_AI : MonoBehaviour
{
    public bool b_initiate = false;
    [Space]
    public Transform target;
    [Space]
    public float moveSpeed;
    public float angularSpeed;
    [Space]
    public float distanceFollow;
    public float distanceEnableAI;



    private NavMeshAgent agent;
    private Animator anim;
    private float cooldownPathFind = 1;
    private float nextPathFind = 0;



    void Start()
    {
        cooldownPathFind = Random.Range(0.7f, 2f);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;

    }//Start



    void Update()
    {
        //Debug.Log(Vector3.Distance(target.position, gameObject.transform.position));
        if (b_initiate == false && Vector3.Distance(target.position, gameObject.transform.position) <= distanceEnableAI)
        {
            b_initiate = true;
            FindObjectOfType<F_Game_Manager>().foxesRequired--;
            FindObjectOfType<F_Game_Manager>().florin_pickup.FOXCollectedAmount++;
            FindObjectOfType<F_Game_Manager>().florin_pickup.b_FOX_allowPanelShowing = true;
        }


        if (b_initiate && target)
        {
            if (Time.time > nextPathFind)
            {
                nextPathFind = Time.time + cooldownPathFind;

                if (Vector3.Distance(target.position, gameObject.transform.position) > distanceFollow)
                {
                    agent.SetDestination(target.position);
                    if (anim.speed == 1) anim.speed = 2;
                    anim.SetInteger("Ana", 1);
                }                

            }

            if (Vector3.Distance(target.position, gameObject.transform.position) <= distanceFollow - 3)
            {
                agent.SetDestination(gameObject.transform.position);
                if (anim.speed == 2) anim.speed = 1;
                anim.SetInteger("Ana", 0);
                Vector3 lookPos = target.position - transform.position;
                Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                float eulerY = lookRot.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
                transform.rotation = rotation;
            }

        }

        
    }//Update


}//END
