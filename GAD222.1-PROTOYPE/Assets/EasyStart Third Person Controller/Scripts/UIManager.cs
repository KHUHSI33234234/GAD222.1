using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Controls UI")]
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI controlsDisplay;

    [Header("Inventory UI")]
    public Image[] inventorySlots;

    void Start()
    {
        // Start showing only "Hold Tab" text
        controlsText.gameObject.SetActive(true);
        controlsDisplay.gameObject.SetActive(false);
    }

    void Update()
    {
        // Show/hide control text depending on TAB key
        if (Input.GetKey(KeyCode.Tab))
        {
            controlsText.gameObject.SetActive(false);
            controlsDisplay.gameObject.SetActive(true);
        }
        else
        {
            controlsText.gameObject.SetActive(true);
            controlsDisplay.gameObject.SetActive(false);
        }
    }

    public void AddToInventory(Sprite pieceSprite)
    {
        foreach (Image slot in inventorySlots)
        {
            if (slot.sprite == null)
            {
                slot.sprite = pieceSprite;
                slot.color = Color.white;
                break;
            }
        }
    }
}