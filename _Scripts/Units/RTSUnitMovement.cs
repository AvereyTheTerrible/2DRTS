using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RTSUnitMovement : NetworkBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Targeter targeter;

    [SerializeField]
    private float chaseRange;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovement { get; set; }

    [field: SerializeField]
    public UnityEvent<bool> IsMovingTowardsDestination { get; set; }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        TryChase();
        TryMove();
    }

    [Command]
    public void CmdMoveToPoint(Vector2 position)
    {
        targeter.ClearTarget();
        agent.SetDestination(position);
        OnMovement?.Invoke(position);
    }

    private void TryMove()
    {
        if (!agent.hasPath) return;

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            IsMovingTowardsDestination?.Invoke(true);
            return;
        }

        agent.ResetPath();
        IsMovingTowardsDestination?.Invoke(false);
    }

    private void TryChase()
    {
        Targetable target = targeter.Target;

        if (target != null)
        {
            if ((target.transform.position - transform.position).sqrMagnitude > chaseRange * chaseRange)
            {
                agent.SetDestination(target.transform.position);
            }

            else if (agent.hasPath)
            {
                agent.ResetPath();
            }

            return;
        }
    }
}
