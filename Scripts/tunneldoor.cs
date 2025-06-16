/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the tunnel door that will open when the character goes close to it.
*/

using UnityEngine;
public class TunnelDoor : MonoBehaviour
{
    /// <summary> door opening sound </summary>
    [SerializeField]
    private AudioClip openSound;

    /// <summary> door to be disabled </summary>
    [SerializeField]
    private GameObject doorObject;

    /// <summary> checks if door is open alrdy </summary>
    private bool isOpen = false;

    /// <summary> opens door </summary>
    public void Interact()
    {
        if (isOpen) return;

        Debug.Log("Door opening...");
        isOpen = true;

        if (openSound != null)
            AudioSource.PlayClipAtPoint(openSound, transform.position);

        if (doorObject != null)
            doorObject.SetActive(false);
    }
}
