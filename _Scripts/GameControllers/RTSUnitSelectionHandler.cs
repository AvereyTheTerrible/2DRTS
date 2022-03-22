using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Events;

public class RTSUnitSelectionHandler : MonoBehaviour
{
    [SerializeField]
    private Transform unitSelectionArea;

    private Vector2 startPosition;
    public List<RTSUnit> selectedRTSUnitsList { get; private set; }

    private void Awake()
    {
        unitSelectionArea.gameObject.SetActive(false);
        selectedRTSUnitsList = new List<RTSUnit>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Left Mouse Button Pressed
            unitSelectionArea.gameObject.SetActive(true);
            startPosition = UtilsClass.GetMouseWorldPosition();

            DeselectRTSUnits();
        }

        else if (Input.GetMouseButton(0))
        {
            RenderSelectionArea();
        }

        else if(Input.GetMouseButtonUp(0))
        {
            // Left Mouse Button Released
            unitSelectionArea.gameObject.SetActive(false);
            unitSelectionArea.localScale = Vector2.zero;

            Collider2D[] collidersInSelection = Physics2D.OverlapAreaAll(
                startPosition, 
                UtilsClass.GetMouseWorldPosition());


            DeselectRTSUnits();
            SelectRTSUnits(collidersInSelection);
        }
    }

    private void SelectRTSUnits(Collider2D[] selectedRTSUnitColliders)
    {
        // Loop through colliders in the selection area
        foreach (var collider in selectedRTSUnitColliders)
        {
            // Is the collider a unit?
            if (!collider.TryGetComponent<RTSUnit>(out RTSUnit unit)) continue;
            selectedRTSUnitsList.Add(unit);
            unit.Select();
        }
    }

    private void DeselectRTSUnits()
    {
        foreach (var selectedUnit in selectedRTSUnitsList)
        {
            selectedUnit.Deselect();
        }
        selectedRTSUnitsList.Clear();
    }

    private void RenderSelectionArea()
    {
        Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 bottomLeft = new Vector3(
            Mathf.Min(startPosition.x, currentMousePosition.x),
            Mathf.Min(startPosition.y, currentMousePosition.y)
        );
        Vector3 topRight = new Vector3(
            Mathf.Max(startPosition.x, currentMousePosition.x),
            Mathf.Max(startPosition.y, currentMousePosition.y)
        );
        unitSelectionArea.position = bottomLeft;
        unitSelectionArea.localScale = topRight - bottomLeft;
    }
}
