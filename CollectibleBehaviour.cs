/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the collectible items in my game, which is all the crystals. When the character
             touches a collectible crystal, it will collect it and increase the score.
*/

using UnityEngine;
public class CollectibleBehaviour : MonoBehaviour
{
    /// <summary> for evry collectible collected, five points is added </summary>
    [SerializeField]
    private int collectibleValue = 5;

    /// <summary> when character goes near, collectible should light up yellow </summary>
    public Color highlightColor = Color.yellow;

    /// <summary> collectibles renderer </summary>
    private Renderer collectibleRenderer;

    private Color originalColor;

    /// <summary> collectibles material and color </summary>
    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();
        collectibleRenderer.material = new Material(collectibleRenderer.material);
        originalColor = collectibleRenderer.material.color;
    }

    /// <summary> lets character collect the crystal </summary>
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

    /// <summary> collects the collectible, adds score to the player and removes it </summary>
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Collectible collected!");
        player.ModifyScore(collectibleValue);
        Destroy(gameObject);
    }

    /// <summary> should highlight the crystal </summary>
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
