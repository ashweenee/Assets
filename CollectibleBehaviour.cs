using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField]
    int coinValue = 1;

    private Renderer collectibleRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();
        collectibleRenderer.material = new Material(collectibleRenderer.material);
        originalColor = collectibleRenderer.material.color;
    }

    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Collectible collected!");
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }

    public void Highlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = highlightColor;
        }
    }

    public void Unhighlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = originalColor;
        }
    }
}
