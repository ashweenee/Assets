using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float interactDistance = 5f;

    bool canInteract = false;

    TunnelDoor currentDoor = null;
    CollectibleBehaviour currentCollectible = null;

    int currentScore = 0;
    int currentHealth;
    [SerializeField] int maxHealth = 100;

    void Start()
    {
        // Auto-assign Main Camera if not assigned manually
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("cameraTransform was auto-assigned to Main Camera.");
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            CollectibleBehaviour collectible = hit.collider.GetComponent<CollectibleBehaviour>();

            if (collectible != null)
            {
                if (currentCollectible != collectible)
                {
                    if (currentCollectible != null)
                        currentCollectible.Unhighlight();

                    collectible.Highlight();
                    currentCollectible = collectible;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    collectible.Collect(this);
                    currentCollectible = null;
                }
            }
            else if (currentCollectible != null)
            {
                currentCollectible.Unhighlight();
                currentCollectible = null;
            }
        }
        else if (currentCollectible != null)
        {
            currentCollectible.Unhighlight();
            currentCollectible = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<TunnelDoor>();
            OnInteract();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    void OnInteract()
    {
        if (currentDoor != null)
        {
            Debug.Log("Interacting with door");
            currentDoor.Interact();
        }
    }

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score: " + currentScore);
    }
}
