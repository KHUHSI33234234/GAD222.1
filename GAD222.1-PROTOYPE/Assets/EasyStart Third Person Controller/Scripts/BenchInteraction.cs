using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BenchInteraction : MonoBehaviour
{
    [Header("Settings")]
    public string puzzleSceneName = "PuzzleScene";  // Scene name to load
    public TMP_Text interactText;                  // TextMeshPro UI text

    [Header("Bench Setup")]
    public bool isSolid = false;                   // True = bench collider solid, false = trigger

    private Collider benchCollider;
    private bool isPlayerNear = false;

    void Start()
    {
        benchCollider = GetComponent<Collider>();
        if (benchCollider == null)
        {
            Debug.LogError("BenchInteraction: No collider found on this bench!");
            return;
        }

        // Set collider mode
        benchCollider.isTrigger = !isSolid;

        // Hide the text at start
        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Optional: make collider trigger after interaction
            benchCollider.isTrigger = true;

            // Load the puzzle scene
            SceneManager.LoadScene(puzzleSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (interactText != null)
                interactText.gameObject.SetActive(true); // Show text
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (interactText != null)
                interactText.gameObject.SetActive(false); // Hide text
        }
    }
}