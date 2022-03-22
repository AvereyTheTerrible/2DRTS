using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitFiring : NetworkBehaviour
{
    [SerializeField]
    private Targeter targeter = null;
    [SerializeField]
    private GameObject projectilePrefab = null;
    [SerializeField]
    private Transform projectileSpawnPoint = null;
    [SerializeField]
    private float fireRange = 3f, fireRate = 1f;

    private float lastFireTime;
    
    [ServerCallback]
    private void Update()
    {
        if (!CanFire()) return;

        Quaternion targetRotation = 
            Quaternion.LookRotation(targeter.Target.transform.position - transform.position);

        if (Time.time > (1 / fireRate) + lastFireTime)
        {
            lastFireTime = Time.time;
        }
    }

    [Server]
    private bool CanFire()
    {
        return (targeter.Target.transform.position - transform.position).sqrMagnitude 
            > fireRange * fireRange;
    }
}
