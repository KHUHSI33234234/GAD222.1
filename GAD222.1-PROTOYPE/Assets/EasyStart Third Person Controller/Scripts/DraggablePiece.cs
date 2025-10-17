using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int pieceID; // must match correct slot
    private Transform originalParent;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // move on top
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool snapped = false;

        // Check all slots
        foreach (PuzzleSlot slot in FindObjectsOfType<PuzzleSlot>())
        {
            if (slot.slotID == pieceID && !slot.isOccupied)
            {
                // Correct slot → snap
                transform.SetParent(slot.transform);
                rectTransform.position = slot.transform.position;
                slot.isOccupied = true;
                snapped = true;

                // Check win condition
                PuzzleManager pm = FindObjectOfType<PuzzleManager>();
                if (pm != null)
                    pm.CheckWinCondition();

                break;
            }
        }

        // If no correct slot → return to inventory
        if (!snapped)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}