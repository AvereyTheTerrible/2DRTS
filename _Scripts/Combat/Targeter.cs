using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    [field: SerializeField]
    public Targetable Target { get; private set; }

    #region Server

    [Command]
    public void CmdSetTarget(GameObject targetGO)
    {
        if (!targetGO.TryGetComponent<Targetable>(out Targetable newTarget)) { return; }

        Target = newTarget;
    }

    [Server]
    public void ClearTarget()
    {
        Target = null;
    }

    #endregion

    #region Client

    #endregion
}
