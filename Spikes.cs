/*
Author: Ashwinii Krishnan
Date: 16th June 2025
Description: Script for the spike hazard that is in my game. The character will die if they touch the spikes. 
             The script is similar to the LavaPit script as both intends to kill the character upon contact.
*/

using UnityEngine;
public class Spikes : MonoBehaviour
{
    /// <summary> character dying to spikes sound </summary>
    [SerializeField]
    private AudioClip spikeSound;

    /// <summary> sees whether character lands in spikes or not </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spikeSound != null)
                AudioSource.PlayClipAtPoint(spikeSound, transform.position);

            Debug.Log("Player hit spikes!");

            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
