using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visual_Path_Finding : MonoBehaviour
{
    public LineRenderer line;
    public Transform target;
    public NavMeshAgent agent;
    public float size = 0.08f;
    [Space]
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public float distanceSwitch = 9;

    void Start()
    {
        target = null;
        target = target1;

        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        TargetManagement();

        if (target == null)
        {
            line.startWidth = 0;
            line.endWidth = 0;
        }

        if (target != null)
        {
            line.startWidth = size;
            line.endWidth = size;

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


    void TargetManagement()
    {
        if(target == target1 && Vector3.Distance(target1.position, gameObject.transform.position) <= distanceSwitch)
        {
            target = target2;
        }

        if (target == target2 && Vector3.Distance(target2.position, gameObject.transform.position) <= distanceSwitch)
        {
            target = target3;
        }

        if (target == target3 && Vector3.Distance(target3.position, gameObject.transform.position) <= distanceSwitch)
        {
            target = target4;
        }

    }//TargetManagement


}//END
