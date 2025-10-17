using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Assign your 6 inventory slot Images here")]
    public Image[] slots;

    private int nextSlot = 0;

    private void Awake()
    {
        // Singleton setup so we can access from anywhere
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this keeps it alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called when a collectible piece is picked up
    public void AddPiece(Sprite piece)
    {
        if (nextSlot >= slots.Length)
        {
            Debug.Log("Inventory full — all puzzle pieces collected!");
            return;
        }

        slots[nextSlot].sprite = piece;
        slots[nextSlot].color = Color.white;  // ensure visible
        nextSlot++;

        // Optional: print to console for debugging
        Debug.Log("Collected piece " + nextSlot);
    }
}