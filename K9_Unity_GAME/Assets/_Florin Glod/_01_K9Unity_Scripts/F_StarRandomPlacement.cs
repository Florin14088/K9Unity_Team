#region using Owner.Florin Glod
//Script created by Florin Glod on February 3, 2020;
//This script cannot be used by any person, entity, or organization without written permission from the creator.
//All rights reserved.
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class F_StarRandomPlacement : MonoBehaviour
{
    public GameObject starGlobe;
    public Rigidbody rb;
    public MeshRenderer meshRend;


    void Start()
    {
        //starGlobe = GameObject.Find("Sky Globe");

        //if (gameObject.GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        //rb.useGravity = false;
        //rb.freezeRotation = true;

        //gameObject.transform.Rotate(Random.Range(-5, -175), Random.Range(0, 360), gameObject.transform.rotation.z, Space.Self);
         //gameObject.transform.Rotate(Random.Range(0, -359), Random.Range(0, 359), gameObject.transform.rotation.z, Space.Self);

        //meshRend = gameObject.GetComponent<MeshRenderer>();
        //meshRend.enabled = false;
    }


    void FixedUpdate()
    {
        rb.velocity = transform.forward * 400;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == starGlobe)
        {
            meshRend.enabled = true;
            Destroy(rb);
            Destroy(GetComponent<Collider>());
            //Destroy(GameObject.Find("Sky Globe"));
            Destroy(gameObject.GetComponent<F_StarRandomPlacement>());
        }
    }


}
