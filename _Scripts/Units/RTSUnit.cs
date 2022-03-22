using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RTSUnit : NetworkBehaviour
{
    [SerializeField]
    private RTSUnitRenderer unitRenderer;

    [field: SerializeField]
    public Targeter Targeter { get; private set; }

    [field: SerializeField]
    public UnityEvent OnSelected { get; private set; }
    [field: SerializeField]
    public UnityEvent OnDeselected { get; private set; }

    public static event Action<RTSUnit> ServerOnUnitSpawned;
    public static event Action<RTSUnit> ServerOnUnitDespawned;

    public static event Action<RTSUnit> AuthorityOnUnitSpawned;
    public static event Action<RTSUnit> AuthorityOnUnitDespawned;

    [field: SerializeField]
    public RTSUnitMovement UnitMovement { get; private set; }

    #region Server

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }

    #endregion

    public override void OnStartClient()
    {
        if (!isClientOnly || !hasAuthority) return;

        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!isClientOnly || !hasAuthority) return;

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!hasAuthority) return;
        
        OnSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!hasAuthority) return;

        OnDeselected?.Invoke();
    }
}
