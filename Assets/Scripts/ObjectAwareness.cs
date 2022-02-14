using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAwareness : MonoBehaviour
{
    private PlayerTransformation playerTransformation;
    public Transform rayTransform;
    private float rayDistance = 50f;

    public GameObject hitGameobject;
    //public LayerMask objectTarget;
    public bool rayHit { get; private set; }

    protected void OnEnable()
    {
        rayHit = false;
    }

    private void Start()
    {
        playerTransformation = GetComponent<PlayerTransformation>();
    }

    public void Awareness()
    {
        RaycastHit hit;
        Vector3 hitposition = Vector3.zero;

        int objectTarget = 1 << LayerMask.NameToLayer("ObjectTarget");
        //objectTarget = ~objectTarget;

        if (Physics.Raycast(rayTransform.transform.position, rayTransform.transform.forward, out hit, rayDistance, objectTarget))
        {   
            rayHit = true;
            hitGameobject = hit.transform.gameObject;

            Debug.DrawRay(rayTransform.transform.position, rayTransform.transform.forward * rayDistance, Color.blue, 0.3f);
            Debug.Log("ObjectTarget ´êÀ½");
                        
        }            
        else
        {
            rayHit = false;

            Debug.Log("´êÁö ¾ÊÀ½");
            
            return;
        }
           
    }
}
