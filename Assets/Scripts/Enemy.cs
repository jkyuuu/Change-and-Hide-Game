using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget;
    //private PlayerTransformation playerTransformation;

    public LivingEntity entityTarget;
    private NavMeshAgent pathFinder;
    public GameObject player;
    public NavMeshAgent nav;
    public Transform targetPos;
    public GameObject transPlayer;

    float setRange = 5f;
    Vector3 setPoint;

    private bool hasTarget
    {
        get
        {
            if(entityTarget != null && !entityTarget.dead)
            {
                return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        //playerTransformation = GameObject.Find("Player").GetComponent<PlayerTransformation>();
        targetPos = GameObject.Find("Target Pos").transform;
        player = GameObject.FindWithTag("Player");
        //transPlayer = GameObject.FindWithTag

        //StartCoroutine(RandMove());
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= 5f )// entityTarget != null)
            {
                StartCoroutine(UpdatePath());
                Debug.Log("플레이어 추적");
            }
            else if (distance > 5f)
            {
                StopCoroutine(UpdatePath());
                RandMove();
            }
            //else if (entityTarget == null)
            //{
            //    StopCoroutine(UpdatePath());
            //    RandMove();
            //}
        }
    }

    public void RandMove()
    {
        if (!dead)
        {  
            if (RandomPoint(targetPos.position, setRange, out setPoint))
            {
                Debug.DrawRay(setPoint, Vector3.up, Color.red, 1.0f);
                targetPos.position = setPoint;
            }
            if (nav != null)
            {
                nav.SetDestination(targetPos.position);
                Debug.Log("랜덤 목적지로 이동");
            } 
        }
        //yield return new WaitForSeconds(0.2f);
    }

    //public void RandMove()
    //{
    //    if (!dead)
    //    {
    //        if (player != null)
    //        {
    //            float distance = Vector3.Distance(transform.position, player.transform.position);

    //            if(distance <= 5f && entityTarget != null)
    //            {
    //                StartCoroutine(UpdatePath());
    //                Debug.Log("플레이어 추적");
    //            }
    //            else if (distance > 5f || entityTarget == null)
    //            {
    //                StopCoroutine(UpdatePath());
                    
    //                if (RandomPoint(targetPos.position, setRange, out setPoint))
    //                {
    //                    Debug.DrawRay(setPoint, Vector3.up, Color.red, 1.0f);
    //                    targetPos.position = setPoint;
    //                }
    //                if (nav != null)
    //                {
    //                    nav.SetDestination(targetPos.position);
    //                    Debug.Log("랜덤 목적지로 이동");
    //                }
    //            }
    //        }
    //    }
    //    //yield return new WaitForSeconds(0.2f);
    //}

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

    private IEnumerator UpdatePath()
    {
        var waitForSec = new WaitForSeconds(0.2f);
        while(!dead)
        {
            if (hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(entityTarget.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, whatIsTarget);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity targetLivingEntity = colliders[i].GetComponent<LivingEntity>();

                    if (targetLivingEntity != null && !targetLivingEntity.dead)
                    {
                        entityTarget = targetLivingEntity;
                        //target = entityTarget.transform;

                        break;
                    }
                }
            }
            //else if(!hasTarget && entityTarget == null)
            //{
            //    RandMove();
            //}
            yield return waitForSec;
        }
    }
}
