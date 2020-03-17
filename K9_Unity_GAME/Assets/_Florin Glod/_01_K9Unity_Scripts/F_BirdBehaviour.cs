using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class F_BirdBehaviour : MonoBehaviour
{
    public GameObject[] waypoints_Bird;
    [Range(0, 9)] public float moveSpeed = 4f;

    [SerializeField] private int waypoint_count = 0;


    void Start()
    {
        //waypoints_Bird = GameObject.FindGameObjectsWithTag("Bird_Way");
        //waypoint_count = waypoints_Bird.Length;

        gameObject.transform.LookAt(waypoints_Bird[Random.Range(0, waypoint_count)].transform.position);


    }//Start


    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }//Update


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bird_Way")
        {
            print("Gaefa");
            gameObject.transform.LookAt(waypoints_Bird[Random.Range(0, waypoint_count)].transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Debug.Log("Intoarce");
            gameObject.transform.LookAt(waypoints_Bird[Random.Range(0, waypoint_count)].transform.position);
        }
    }



}//END
