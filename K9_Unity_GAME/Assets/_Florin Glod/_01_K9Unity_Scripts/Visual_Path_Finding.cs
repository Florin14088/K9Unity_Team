using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visual_Path_Finding : MonoBehaviour
{
    public LineRenderer line;
    public Transform target;
    public NavMeshAgent agent;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (target == null)
        {
            line.startWidth = 0;
            line.endWidth = 0;
        }

        if (target != null)
        {
            line.startWidth = 0.08f;
            line.endWidth = 0.08f;

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

}//END
