/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the collectible items in my game, which is all the crystals. When the character
             touches a collectible crystal, it will collect it and increase the score.
*/

using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField]
    int collectibleValue = 5;

    private Renderer collectibleRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();
        collectibleRenderer.material = new Material(collectibleRenderer.material);
        originalColor = collectibleRenderer.material.color;
    }

    // Add this method to detect when player touches the collectible
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered collectible trigger: " + other.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Attempting to collect...");
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                Collect(player);
            }
            else
            {
                Debug.Log("PlayerBehaviour component not found!");
            }
        }
        else
        {
            Debug.Log("Object tag: " + other.tag);
        }
    }

    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Collectible collected!");
        player.ModifyScore(collectibleValue);
        Destroy(gameObject);
    }

    public void Highlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = highlightColor;
        }
    }

    public void Unhighlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = originalColor;
        }
    }
}