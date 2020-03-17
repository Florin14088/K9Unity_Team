using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class F_Wander_AI : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    [Space]
    public Animator anim;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private Vector3 newPos;



    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }


    private void Start()
    {

    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        //print(Vector3.Distance(newPos, gameObject.transform.position));
        if(Vector3.Distance(newPos, gameObject.transform.position) <= 2f)
        {
            anim.SetInteger("Ana", 1);
            agent.SetDestination(gameObject.transform.position);
        }
        else anim.SetInteger("Ana", 2);

    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


}//END
