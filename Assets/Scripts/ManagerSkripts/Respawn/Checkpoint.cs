using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager checkpointManager;
    private bool isReached = false;

    public Sprite[] sprites;

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
            spriteRenderer.sprite = sprites[0];
            spriteRenderer.color = Color.yellow;
            FindObjectOfType<AudioManager>().PlaySound("CheckPointAberEigDash");
            checkpointManager.SetLastCheckpoint(this);
            isReached = true;
            // Weitere Aktionen, z.B. Aktivieren des Checkpoint-Sprites
        }
    }
}
