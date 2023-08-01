using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    //TODO: 
    // Enemy respawnen wenn sie schon tot sind funktioniert noch nicht richtig!!!!!!!!!!!!!

    
    public Transform respawnPoint;
    [SerializeField]
    private Vector2 respawnPointEnemy;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    public bool respawn;


    private CinemachineVirtualCamera cinemachineVirtualCamera;

    //[SerializeField]
    //private GameObject enemy;

    //public Entity deadState;

    private void Start()
    {

        cinemachineVirtualCamera = GameObject.Find("OpenRoomMainCamera").GetComponent<CinemachineVirtualCamera>();
        //deadState = GetComponent<Entity>();
    }

    private void Update()
    {
        CheckRespawn();
        //CheckRespawnEnemy();
        respawnPoint.localScale = Vector3.one;
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;

    }

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {

            var playerTemp = Instantiate(player, respawnPoint);
            cinemachineVirtualCamera.m_Follow = playerTemp.transform;
            respawn = false;

        }

    }

    //public void CheckRespawnEnemy()
    //{
    //    if (Time.time >= respawnTimeStart + respawnTime && respawn)
    //    {
    //        Debug.Log("lol");
    //        var enemyTemp = Instantiate(enemy, deadState.EnemyRespawn);

    //        respawn = false;

    //    }

    //}


}
