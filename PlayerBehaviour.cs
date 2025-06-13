using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private bool canInteract = false;
    private CollectibleBehaviour currentCollectible = null;
    private TunnelDoor currentDoor = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentCollectible = other.GetComponent<CollectibleBehaviour>();
            if (currentCollectible != null)
            {
                currentCollectible.Collect();
            }
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<TunnelDoor>();
            if (currentDoor != null)
            {
                OnInteract();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible") && currentCollectible != null)
        {
            canInteract = false;
            currentCollectible = null;
        }

        if (other.CompareTag("Door") && currentDoor != null)
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    private void OnInteract()
    {
        if (currentCollectible != null)
        {
            currentCollectible.Collect();
        }

        if (currentDoor != null)
        {
            currentDoor.Interact();
        }
    }
}

