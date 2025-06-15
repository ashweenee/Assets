using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    AudioClip spikeSound;

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