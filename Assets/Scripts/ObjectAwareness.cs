using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAwareness : MonoBehaviour
{
    public Transform rayTransform;
    private float rayDistance = 50f;

    //public LayerMask objectTarget;

    private void Click()
    {
        RaycastHit hit;
        Vector3 hitposition = Vector3.zero;

        if(Physics.Raycast(rayTransform.position, rayTransform.forward, out hit, rayDistance))
        {
            ObjectTarget target = hit.collider.GetComponent<ObjectTarget>();
        }
    }
}
