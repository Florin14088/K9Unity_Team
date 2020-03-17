using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class F_Visual_Path_Finding : MonoBehaviour
{
    public LineRenderer line;
    public Transform target;
    public NavMeshAgent agent;
    [Space]
    [Range(0.08f, 1f)] public float size_line = 0.08f;
    [Space]
    public Transform fox_target_1;
    public Transform fox_target_2;
    public Transform fox_target_3;
    public Transform den_target_4;
    public bool b_ignore_T1 = false;
    public bool b_ignore_T2 = false;
    public bool b_ignore_T3 = false;


    private GameObject parent;
    private int randomT = 0;
    private float distance_1;
    private float distance_2;
    private float distance_3;



    void Start()
    {
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();

        parent = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {     
        CalculateDistance();

        if (target == null)
        {
            line.startWidth = 0;
            line.endWidth = 0;
        }

        if (target != null)
        {
            line.startWidth = size_line;
            line.endWidth = size_line;

            line.SetPosition(0, transform.position); //gameObject.transform.position;
            agent.SetDestination(target.position);
            //yield return new WaitForEndOfFrame();

            DrawPath(agent.path);
        }

    }


    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return;

        line.SetVertexCount(path.corners.Length);

        for (int i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
    }


    void CalculateDistance()
    {
        distance_1 = Vector3.Distance(parent.transform.position, fox_target_1.position);
        distance_2 = Vector3.Distance(parent.transform.position, fox_target_2.position);
        distance_3 = Vector3.Distance(parent.transform.position, fox_target_3.position);

        if (distance_1 <= 10) b_ignore_T1 = true;
        if (distance_2 <= 10) b_ignore_T2 = true;
        if (distance_3 <= 10) b_ignore_T3 = true;

        if (target != fox_target_1 && b_ignore_T1 == false) target = fox_target_1; 
        if (target != fox_target_2 && b_ignore_T1 && b_ignore_T2 == false) target = fox_target_2; 
        if (target != fox_target_3 && b_ignore_T1 && b_ignore_T2 && b_ignore_T3 == false) target = fox_target_3; 

        if (b_ignore_T1 && b_ignore_T2 && b_ignore_T3)
        {
            //target = null;
            //gameObject.GetComponent<Visual_Path_Finding>().enabled = false;
            target = den_target_4;
        }


    }//CalculateDistance


}//END
