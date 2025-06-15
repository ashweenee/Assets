using UnityEngine;

public class LavaPit : MonoBehaviour
{
    [SerializeField]
    AudioClip sizzleSound;

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
