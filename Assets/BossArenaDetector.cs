using UnityEngine;

public class BossArenaDetector : MonoBehaviour
{
    public BossHealthbarUI bossHealthbarUI; // Verweise hier auf dein UI-Skript

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossHealthbarUI.ShowHealthBar(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossHealthbarUI.ShowHealthBar(false);
        }
    }
}
