using UnityEngine;
using UnityEngine.UI; // Add this for UI components

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Transform cameraTransform;

    [Header("Interaction")]
    [SerializeField] float interactDistance = 5f;

    [Header("Game Stats")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int totalCollectibles = 5;

    [Header("UI References")]
    [SerializeField] GameObject deathScreenUI;
    [SerializeField] GameObject winScreenUI;
    [SerializeField] Text scoreText; // Add this line - for displaying current score

    [Header("Respawn")]
    [SerializeField] Transform respawnPoint;

    // Private variables
    private bool canInteract = false;
    private TunnelDoor currentDoor = null;
    private CollectibleBehaviour currentCollectible = null;
    private int currentScore = 0;
    private int currentHealth;
    private int collectiblesCollected = 0;

    void Start()
    {
        // Auto-assign Main Camera if not assigned manually
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("cameraTransform was auto-assigned to Main Camera.");
        }

        currentHealth = maxHealth;

        // Hide UI screens at start
        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);
        if (winScreenUI != null)
            winScreenUI.SetActive(false);

        // Initialize score display
        UpdateScoreDisplay();
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            CollectibleBehaviour collectible = hit.collider.GetComponent<CollectibleBehaviour>();

            if (collectible != null)
            {
                if (currentCollectible != collectible)
                {
                    if (currentCollectible != collectible)
                        currentCollectible.Unhighlight();

                    collectible.Highlight();
                    currentCollectible = collectible;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    collectible.Collect(this);
                    currentCollectible = null;
                }
            }
            else if (currentCollectible != null)
            {
                currentCollectible.Unhighlight();
                currentCollectible = null;
            }
        }
        else if (currentCollectible != null)
        {
            currentCollectible.Unhighlight();
            currentCollectible = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<TunnelDoor>();
            OnInteract();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    void OnInteract()
    {
        if (currentDoor != null)
        {
            Debug.Log("Interacting with door");
            currentDoor.Interact();
        }
    }

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        collectiblesCollected++;
        Debug.Log("Score: " + currentScore + " | Collectibles: " + collectiblesCollected + "/" + totalCollectibles);

        // Update the UI display
        UpdateScoreDisplay();

        // Check if all collectibles are collected
        if (collectiblesCollected >= totalCollectibles)
        {
            ShowWinScreen();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Collectibles: " + collectiblesCollected + "/" + totalCollectibles + " | Score: " + currentScore;
            Debug.Log("UI Updated: " + scoreText.text); // Add this line
        }
        else
        {
            Debug.LogError("scoreText is null!"); // Add this line
        }
    }
    private void ShowWinScreen()
    {
        Debug.Log("All collectibles collected! Showing win screen.");
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void Die()
    {
        Debug.Log("Player died.");

        if (deathScreenUI != null)
            deathScreenUI.SetActive(true);

        // Don't disable the player GameObject immediately
        // Just stop player movement/input instead
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        Invoke(nameof(Respawn), 2f);
    }

    void Respawn()
    {
        Debug.Log("Respawning player.");

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("No respawn point assigned!");
        }

        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);

        // Reset health
        currentHealth = maxHealth;
    }

    // Public method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}