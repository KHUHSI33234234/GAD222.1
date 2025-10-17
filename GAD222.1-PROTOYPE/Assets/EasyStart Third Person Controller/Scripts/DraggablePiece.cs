using UnityEngine;
using UnityEngine.UI;               // <- needed for GraphicRaycaster
using UnityEngine.EventSystems;     // <- needed for IDragHandler, PointerEventData
using System.Collections.Generic;   // <- needed for List<RaycastResult>

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

        // Perform a raycast to detect UI elements under the pointer
        Canvas canvas = GetComponentInParent<Canvas>();
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var r in results)
        {
            PuzzleSlot slot = r.gameObject.GetComponent<PuzzleSlot>();
            if (slot != null && slot.slotID == pieceID && !slot.isOccupied)
            {
                // Correct slot under pointer → snap
                transform.SetParent(slot.transform);
                rectTransform.position = slot.transform.position;
                slot.isOccupied = true;
                snapped = true;

                // Check win condition
                PuzzleManager pm = FindObjectOfType<PuzzleManager>();
                if (pm != null) pm.CheckWinCondition();

                break;
            }
        }

        // If no correct slot under pointer → return to inventory
        if (!snapped)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
