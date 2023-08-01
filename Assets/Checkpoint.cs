using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameManager GameManager;

    private SpriteRenderer sprite;

    private void Awake()
    {
        GameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
        sprite =GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hey bonfire");
        if (collision.tag == "Player")
        {
            Debug.Log("hey bonfire");
            sprite.color = Color.white;
            GameManager.respawnPoint = this.transform;
        }
    }
}
