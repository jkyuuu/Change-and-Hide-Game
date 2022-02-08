using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    private void Awake()
    {
        Rigidbody objectRigid = gameObject.GetComponent<Rigidbody>();
        objectRigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
