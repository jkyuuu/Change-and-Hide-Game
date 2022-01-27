using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAwareness : MonoBehaviour
{
    public Transform rayTransform;
    private float rayDistance = 50f;

    public LayerMask objectTarget;


    public void Update()
    {
        Awareness();
    }

    public void Awareness()
    {
        RaycastHit hit;
        Vector3 hitposition = Vector3.zero;

        int objectTarget = 1 << LayerMask.NameToLayer("ObjectTarget");
        //objectTarget = ~objectTarget;

        if(Physics.Raycast(rayTransform.transform.position, rayTransform.transform.forward, out hit, rayDistance, objectTarget))
        {
            //ObjectTarget target = hit.collider.GetComponent<ObjectTarget>();

            //GameObject target = hit.transform.GetComponent<GameObject>();
            //if(target != null)
            //{
            //    target.gameObject
            //}

            Debug.DrawRay(rayTransform.transform.position, rayTransform.transform.forward * rayDistance, Color.blue, 0.3f);
            Debug.Log("ObjectTarget ¥Í¿Ω");
        }
        else
        {
            Debug.Log("¥Í¡ˆ æ ¿Ω");
        }
       
    }
}
