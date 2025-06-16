/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the tunnel door that will open when the character goes close to it.
*/

using UnityEngine;

public class TunnelDoor : MonoBehaviour
{
    [SerializeField]
    AudioClip openSound;

    [SerializeField]
    GameObject doorObject;

    bool isOpen = false;

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
