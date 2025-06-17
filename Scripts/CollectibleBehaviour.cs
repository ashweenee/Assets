/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the collectible items in my game, which is all the crystals. When the character
             touches a collectible crystal, it will collect it and increase the score.
*/

using UnityEngine;
public class CollectibleBehaviour : MonoBehaviour
{
    /// for evry collectible collected, five points is added 
    [SerializeField]
    private int collectibleValue = 5;

    /// when character goes near, collectible should light up yellow 
    public Color highlightColor = Color.yellow;

    ///collectibles renderer 
    private Renderer collectibleRenderer;

    private Color originalColor;

    /// collectibles material and color 
    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();
        collectibleRenderer.material = new Material(collectibleRenderer.material);
        originalColor = collectibleRenderer.material.color;
    }

    /// lets character collect the crystal 
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

    /// collects the collectible, adds score to the player and removes it 
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Collectible collected!");
        player.ModifyScore(collectibleValue);
        Destroy(gameObject);
    }

    /// should highlight the crystal
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
