using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField]
    private List<RTSUnit> myUnits = new List<RTSUnit>();

    #region Server

    public override void OnStartServer()
    {
        RTSUnit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        RTSUnit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        RTSUnit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        RTSUnit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(RTSUnit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

        myUnits.Add(unit);
    }

    private void ServerHandleUnitDespawned(RTSUnit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

        myUnits.Remove(unit);
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        if (!isClientOnly) return;

        RTSUnit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
        RTSUnit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) return;

        RTSUnit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        RTSUnit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }

    private void AuthorityHandleUnitSpawned(RTSUnit unit)
    {
        if (!hasAuthority) return;

        myUnits.Add(unit);
    }

    private void AuthorityHandleUnitDespawned(RTSUnit unit)
    {
        if (!hasAuthority) return;

        myUnits.Remove(unit);
    }

    #endregion
}
