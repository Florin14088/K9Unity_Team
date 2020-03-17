using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class F_Fish_Behaviour : MonoBehaviour
{
    public GameObject[] waypoints_fish;
    [Range(0, 5)] public float moveSpeed = 4f;

    [SerializeField] private int waypoint_count = 0;

    
    void Start()
    {
        //waypoints_fish = GameObject.FindGameObjectsWithTag("FishWaypoint");
        //waypoint_count = waypoints_fish.Length;

        gameObject.transform.LookAt(waypoints_fish[Random.Range(0, waypoint_count)].transform.position);

        
    }//Start


    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }//Update


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FishWaypoint")
        {
            gameObject.transform.LookAt(waypoints_fish[Random.Range(0, waypoint_count)].transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Debug.Log("Intoarce");
            gameObject.transform.LookAt(waypoints_fish[Random.Range(0, waypoint_count)].transform.position);
        }
    }


}//END
