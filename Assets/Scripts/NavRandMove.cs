using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRandMove : LivingEntity
{
    public NavMeshAgent nav;
    public Transform targetPos;

    float setRange = 5f;
    Vector3 setPoint;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        targetPos = GameObject.Find("Target Pos").transform;
    }

    public IEnumerator RandMove()
    {
        if (!dead)
        {
            if (RandomPoint(targetPos.position, setRange, out setPoint))
            {
                Debug.DrawRay(setPoint, Vector3.up, Color.red, 1.0f);
                targetPos.position = setPoint;

            }
            if(nav != null)
            {
                nav.SetDestination(targetPos.position);
                Debug.Log("랜덤 목적지로 이동");
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randPoint = center + Random.insideUnitSphere * range;
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(randPoint, out navHit, 1f, NavMesh.AllAreas))
            {
                result = navHit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


    //NavMeshAgent nav;
    //public Transform targetPos;

    //float range = 10;
    //Vector3 point;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    nav = GetComponent<NavMeshAgent>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        if (RandomPoint(targetPos.position, range, out point))
    //        {
    //            targetPos.position = point;
    //        }
    //    }
    //    nav.SetDestination(targetPos.position);
    //}

    //bool RandomPoint(Vector3 center, float range, out Vector3 result)
    //{
    //    for (int i = 0; i < 30; i++)
    //    {
    //        Vector3 randomPoint = center + Random.insideUnitSphere * range;
    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
    //        {
    //            result = hit.position;
    //            return true;
    //        }
    //    }
    //    result = Vector3.zero;
    //    return false;
    //}
}
