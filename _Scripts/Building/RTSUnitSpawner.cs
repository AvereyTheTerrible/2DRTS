using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RTSUnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject unitPrefab = null;

    [SerializeField]
    private Transform unitSpawnPoint = null;

    [SerializeField]
    private float maxOffset;

    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        float randomOffsetValue = Random.Range(-maxOffset, maxOffset);
        Vector2 randomOffset = new Vector2(randomOffsetValue, randomOffsetValue);

        GameObject unitInstance = Instantiate(
            unitPrefab,
            unitSpawnPoint.position + (Vector3)randomOffset,
            unitSpawnPoint.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient);
    }

    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (!hasAuthority) return;

        CmdSpawnUnit();
    }

    #endregion
}
