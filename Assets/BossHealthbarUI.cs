using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbarUI : MonoBehaviour
{
    public Slider healthSlider;
    public Image fillImage; // Referenz auf das FillImage des Sliders
    public TextMeshProUGUI healthText;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Aktualisiere die Healthbar basierend auf den Healthwerten
        float healthPercentage = currentHealth / maxHealth;
        healthSlider.value = healthPercentage;

        // Zeige den Health-Text im Format "currentHealth/maxHealth" an
        healthText.text = $"{maxHealth}/{currentHealth}";
    }

    public void ShowHealthBar(bool show)
    {
        // Zeige oder blende die Healthbar basierend auf "show" ein oder aus
        healthText.gameObject.SetActive(show);
        healthSlider.gameObject.SetActive(show);
    }

    private void Update()
    {
        //Wenn low on Health
        if (healthSlider.value <= healthSlider.minValue)
        {
            healthText.enabled = false;
            fillImage.enabled = false;
        }

        if (healthSlider.value > healthSlider.minValue && !fillImage.enabled)
        {
            healthText.enabled = true;
            fillImage.enabled = true;
        }
    }
}
