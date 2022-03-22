using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Events;

public class RTSUnitCommander : MonoBehaviour
{
    [SerializeField]
    private RTSUnitSelectionHandler unitSelectionHandler = null;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMoveCommandIssued { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Right Mouse Button Pressed
            Vector2 mousePosition = UtilsClass.GetMouseWorldPosition();

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward);

            if (hit)
            {
                if (hit.collider.TryGetComponent<Targetable>(out Targetable target))
                {
                    if (target.hasAuthority)
                    {
                        MoveUnits(mousePosition);
                        return;
                    }

                    TargetUnits(target);
                }

            }

            if (unitSelectionHandler.selectedRTSUnitsList.Count != 0)
            {
                OnMoveCommandIssued?.Invoke(mousePosition);
            }

            MoveUnits(mousePosition);
        }
    }

    private void MoveUnits(Vector2 targetPosition)
    {
        foreach (var unit in unitSelectionHandler.selectedRTSUnitsList)
        {
            unit.UnitMovement.CmdMoveToPoint(targetPosition);
        }
    }

    private void TargetUnits(Targetable target)
    {
        foreach (var unit in unitSelectionHandler.selectedRTSUnitsList)
        {
            unit.Targeter.CmdSetTarget(target.gameObject);
        }
    }
}
