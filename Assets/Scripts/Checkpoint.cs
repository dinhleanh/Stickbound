using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager checkpointManager;
    private bool isReached = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        checkpointManager = FindObjectOfType<CheckPointManager>();
        checkpointManager.RegisterCheckpoint(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isReached)
        {
            spriteRenderer.color = Color.yellow;
            checkpointManager.SetLastCheckpoint(this);
            isReached = true;
            // Weitere Aktionen, z.B. Aktivieren des Checkpoint-Sprites
        }
    }
}
