/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for my character's behaviour in my game. Theres player interactions like with the door, player dying and respawning, and UI things.
*/

using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    // Header Variables 

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Interaction")]
    [SerializeField] private float interactDistance = 5f;

    /// <summary> health value for the character </summary>
    [Header("Health Stats")]
    [SerializeField] private int maxHealth = 100;

    /// <summary> total number of collectibles needed to win </summary>
    [SerializeField] private int totalCollectibles = 5;

    /// <summary> UI tht is supposed to show when character dies </summary>
    [Header("UI Updates")]
    [SerializeField] private GameObject deathScreenUI;

    /// <summary> UI that is supposed to show up when all collectibles are collected </summary>
    [SerializeField] private GameObject winScreenUI;

    /// <summary> the UI thats supposed to show the current score and collectible count </summary>
    [SerializeField] private Text scoreText;

    /// <summary> respawn and spawn point </summary>
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    /// <summary> character dying noise </summary>
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;

    /// <summary> character winning noise </summary>
    [SerializeField] private AudioClip winSound;

    // Private Variables 

    /// <summary> for character to open door </summary>
    private bool canInteract = false;

    /// <summary> for character to open door </summary>
    private TunnelDoor currentDoor = null;

    /// <summary> for collectible crystals </summary>
    private CollectibleBehaviour currentCollectible = null;

    /// <summary> for current score of the player/character </summary>
    private int currentScore = 0;

    /// <summary> current health of the character </summary>
    private int currentHealth;

    /// <summary> number of collectibles the characater collected </summary>
    private int collectiblesCollected = 0;

    // All the Initialising

    /// <summary> setting up score display </summary>
    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("cameraTransform was auto-assigned to Main Camera.");
        }

        currentHealth = maxHealth;

        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);
        if (winScreenUI != null)
            winScreenUI.SetActive(false);

        UpdateScoreDisplay();
    }

    /// <summary> detects when the character is near a door </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<TunnelDoor>();
            OnInteract();
        }
    }

    /// <summary> detects when character leaves the door area </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    // The Game Part

    /// <summary> character interacting with a door </summary>
    void OnInteract()
    {
        if (currentDoor != null)
        {
            Debug.Log("Interacting with door");
            currentDoor.Interact();
        }
    }

    /// <summary> updates character score and checks is they won </summary>
    public void ModifyScore(int amount)
    {
        currentScore += amount;
        collectiblesCollected++;
        Debug.Log("Score: " + currentScore + " | Collectibles: " + collectiblesCollected + "/" + totalCollectibles);
        UpdateScoreDisplay();

        if (collectiblesCollected >= totalCollectibles)
        {
            ShowWinScreen();
        }
    }

    /// <summary> updates the score and collectibles colected </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Collectibles: " + collectiblesCollected + "/" + totalCollectibles + " | Score: " + currentScore;
            Debug.Log("UI Updated: " + scoreText.text);
        }
        else
        {
            Debug.LogError("scoreText is null!");
        }
    }

    /// <summary> pauses game once character collected all the five collectibels </summary>
    private void ShowWinScreen()
    {
        Debug.Log("All collectibles collected! Showing win screen.");

        if (winSound != null)
            AudioSource.PlayClipAtPoint(winSound, transform.position);

        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /// <summary> kills character and makes them respawn </summary>
    public void Die()
    {
        Debug.Log("Player died.");

        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        if (deathScreenUI != null)
            deathScreenUI.SetActive(true);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Invoke(nameof(Respawn), 2f);
    }

    /// <summary> makes the character respawn at the spawn point </summary>
    void Respawn()
    {
        Debug.Log("Respawning player.");

        if (respawnPoint != null)
        {
            Debug.Log("Moving player to respawn point at: " + respawnPoint.position);

            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                transform.position = respawnPoint.position;
                transform.rotation = respawnPoint.rotation;
                cc.enabled = true;
            }
            else
            {
                transform.position = respawnPoint.position;
                transform.rotation = respawnPoint.rotation;
            }
        }
        else
        {
            Debug.LogWarning("No respawn point assigned!");
        }

        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);

        currentHealth = maxHealth;
    }

    /// <summary> reloads the current scene </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
