/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the lava pit hazard that is in my game. The character will die if they accidentally land in the lava. 
             The script is similar to the Spikes script as both intends to kill the character upon contact.
*/

using UnityEngine;
public class LavaPit : MonoBehaviour
{
    /// <summary> character dying in lava sound </summary>
    [SerializeField]
    private AudioClip sizzleSound;

    /// <summary> sees when character falls in lava </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sizzleSound != null)
                AudioSource.PlayClipAtPoint(sizzleSound, transform.position);

            Debug.Log("Player fell into lava!");

            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}

