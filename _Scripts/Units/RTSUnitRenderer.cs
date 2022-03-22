using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitRenderer : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject selectionHighlightGO;

    public void FlipSprite(Vector2 targetPosition)
    {
        Vector3 direction = (Vector3)targetPosition - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, direction);

        if (result.z > 0)
            transform.localScale = new Vector2(-1, 1);

        else if (result.z < 0)
            transform.localScale = new Vector2(1, 1);
    }

    public void AnimateUnit(bool val)
    {
        animator.SetBool("Run", val);
    }

    public void RenderSelectionHighlight(bool selected)
    {
        selectionHighlightGO.SetActive(selected);
    }
}
