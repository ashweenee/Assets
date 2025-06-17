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

    /// health value for the character 
    [Header("Health Stats")]
    [SerializeField] private int maxHealth = 100;

    /// total number of collectibles needed to win
    [SerializeField] private int totalCollectibles = 5;

    ///  UI tht is supposed to show when character dies
    [Header("UI Updates")]
    [SerializeField] private GameObject deathScreenUI;

    ///  UI that is supposed to show up when all collectibles are collected 
    [SerializeField] private GameObject winScreenUI;

    ///  the UI thats supposed to show the current score and collectible count 
    [SerializeField] private Text scoreText;

    ///  respawn and spawn point 
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    ///  character dying noise 
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;

    /// character winning noise 
    [SerializeField] private AudioClip winSound;

    // Private Variables 

    /// for character to open door 
    private bool canInteract = false;

    /// for character to open door 
    private TunnelDoor currentDoor = null;

    /// for collectible crystals 
    private CollectibleBehaviour currentCollectible = null;

    /// for current score of the player/character 
    private int currentScore = 0;

    /// current health of the character
    private int currentHealth;

    /// number of collectibles the characater collected 
    private int collectiblesCollected = 0;

    // All the Initialising

    /// setting up score display 
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

    /// detects when the character is near a door 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<TunnelDoor>();
            OnInteract();
        }
    }

    ///detects when character leaves the door area 
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    // The Game Part

    /// character interacting with a door 
    void OnInteract()
    {
        if (currentDoor != null)
        {
            Debug.Log("Interacting with door");
            currentDoor.Interact();
        }
    }

    /// updates character score and checks is they won 
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

    /// updates the score and collectibles colected 
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

    /// pauses game once character collected all the five collectibels
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

    /// kills character and makes them respawn 
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

    /// makes the character respawn at the spawn point 
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

    /// reloads the current scene 
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
