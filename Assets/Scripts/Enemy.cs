using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget;

    private LivingEntity entityTarget;
    private NavMeshAgent pathFinder;

    private PlayerTransformation playerTransformation;

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
        playerTransformation = FindObjectOfType<PlayerTransformation>();
    }
    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    private IEnumerator UpdatePath()
    {
        //GameObject firstPlayerTarget = playerTransformation.playerObject;

        while(!dead)
        {
            if(hasTarget) //&& firstPlayerTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(entityTarget.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, whatIsTarget);

                for(int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity targetLivingEntity = colliders[i].GetComponent<LivingEntity>();

                    if(targetLivingEntity != null && !targetLivingEntity.dead)
                    {
                        entityTarget = targetLivingEntity;

                        break;
                    } 
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}
