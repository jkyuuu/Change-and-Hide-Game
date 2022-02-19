using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    //private PlayerTransformation playerTransformation;
    private Rigidbody objectRigid;
    //private GameObject other;

    private void Awake()
    {
        objectRigid = gameObject.GetComponent<Rigidbody>();
        objectRigid.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void Start()
    {
        //playerTransformation = GetComponent<PlayerTransformation>();
    }
    private void Update()
    {
        //if(playerTransformation.hitObject != null)
        //{
        //    other = playerTransformation.gameObject;

        //    other.GetComponent<PlayerInput>();
        //    other.GetComponent<PlayerTransformation>();
        //    other.GetComponent<PlayerMove>();

        //    objectRigid.constraints = RigidbodyConstraints.None;
        //    objectRigid.constraints = RigidbodyConstraints.FreezeRotationX;
        //    objectRigid.constraints = RigidbodyConstraints.FreezeRotationZ;
        //}
    }
}
