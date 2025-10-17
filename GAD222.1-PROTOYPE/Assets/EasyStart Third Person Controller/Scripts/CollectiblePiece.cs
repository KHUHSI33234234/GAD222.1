using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    [Header("Assign the sprite for this piece here")]
    public Sprite pieceSprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add this piece to the inventory system
            if (InventoryManager.instance != null)
            {
                InventoryManager.instance.AddPiece(pieceSprite);
            }

            // Optional: Play a pickup sound or particle effect later
            // AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Remove piece from scene after collecting
            Destroy(gameObject);
        }
    }
}