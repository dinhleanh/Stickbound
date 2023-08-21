using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float maxHealth;

    public float currentHealth;

    

    private GameManager gameManager;


    public float invulnerabilityDuration = 2.0f; // Dauer der Unverwundbarkeit in Sekunden
    public bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        ClampHealth();
       // Debug.Log(currentHealth);

    }


    private void ClampHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }




    public void DecreaseHealth(float amount)
    {

        if (!isInvulnerable)
        {
            currentHealth -= amount;
            Debug.Log("Current Health: " + currentHealth);

            if (currentHealth <= 0.0f)
            {
                Die();
            }
            else
            {
                StartInvulnerability();
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void Die()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        //transform.position = new Vector3(1, 1, 1);
        gameManager.Respawn();
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void StartInvulnerability()
    {
        isInvulnerable = true;
        
        Invoke("EndInvulnerability", invulnerabilityDuration);
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
    }

    public float ShowCurrentHealth()
    {
       
        return currentHealth;
    }

}
