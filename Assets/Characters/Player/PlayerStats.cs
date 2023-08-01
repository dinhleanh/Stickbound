using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;



    private GameManager gameManager;





    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

   
    public void DecreaseHealth(float amount)
    {
        
        currentHealth -= amount;
        Debug.Log(currentHealth);

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        gameManager.Respawn();
        gameObject.SetActive(false);
    }
}
