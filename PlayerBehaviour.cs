using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;

    int currentHealth;
    int currentScore = 0;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float interactDistance = 5f;

    CollectibleBehaviour currentCollectible;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
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

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score: " + currentScore);
    }
}
