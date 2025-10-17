using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("All puzzle slots")]
    public PuzzleSlot[] allSlots;

    [Header("Win Text")]
    public GameObject winText;

    private void Start()
    {
        if (winText != null)
            winText.SetActive(false);
    }

    public void CheckWinCondition()
    {
        foreach (PuzzleSlot slot in allSlots)
        {
            if (!slot.isOccupied)
                return; // not all pieces placed yet
        }

        // All slots occupied → show win text
        if (winText != null)
            winText.SetActive(true);
    }
}