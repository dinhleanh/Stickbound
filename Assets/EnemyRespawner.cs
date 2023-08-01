using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    public Transform m_transform;

    public GameManager manager;

    public GameObject childenemy;

    public bool hasbeenSpawned = false;

    GameObject tempGameObject;
    Transform tempTransform;
    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_transform = GetComponent<Transform>();
        Debug.Log(m_transform);
        childenemy = GetComponentInChildren<GameObject>();


        tempGameObject = childenemy;
        tempTransform = m_transform;
    }


    public void Update()
    {
        if (manager.respawn && !hasbeenSpawned)
        {
            Debug.Log(manager.respawn);
            Debug.Log(childenemy);
            Debug.Log(m_transform);

            Instantiate(tempGameObject, tempTransform);
            hasbeenSpawned = true;
        }


    }
}
