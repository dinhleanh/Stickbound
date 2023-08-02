using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public CheckPointManager checkPointManager;
    
    //public Transform respawnPoint;
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    public bool respawn;

    private Vector3 originalPlayerScale;

    private CinemachineVirtualCamera cinemachineVirtualCamera;




   public  List <Enemy1> enemyRespawner;




    private void Start()
    {

        cinemachineVirtualCamera = GameObject.Find("OpenRoomMainCamera").GetComponent<CinemachineVirtualCamera>();
        


    }

    private void Update()
    {
        CheckRespawn();

        //respawnPoint.localScale = Vector3.one;
        
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        
        originalPlayerScale = player.transform.localScale;
        respawn = true;

    }

    private void CheckRespawn()
    {
        
        



        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            foreach (Enemy1 enemy in enemyRespawner)
            {
                enemy.Respawn();
            }

            // Instanziere das Prefab
            GameObject playerClone = Instantiate(player, checkPointManager.GetLastCheckpointPosition(), Quaternion.identity);

            // Stelle die ursprüngliche Skalierung des Spielers im Klon wieder her
            playerClone.transform.localScale = originalPlayerScale;

            
            cinemachineVirtualCamera.m_Follow = playerClone.transform;
            respawn = false;

        }

    }

   


}
