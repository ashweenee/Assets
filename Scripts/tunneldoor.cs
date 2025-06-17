/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the tunnel door that will open when the character goes close to it.
*/

using UnityEngine;
public class TunnelDoor : MonoBehaviour
{
    /// door opening sound 
    [SerializeField]
    private AudioClip openSound;

    /// door to be disabled
    [SerializeField]
    private GameObject doorObject;

    /// checks if door is open alrdy 
    private bool isOpen = false;

    /// opens door 
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
