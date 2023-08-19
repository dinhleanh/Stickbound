using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public CheckPointManager checkPointManager;
    
    //public Transform respawnPoint;
    
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCombatTry playerCombat;
    [SerializeField]
    private MovementPlayer playerMove;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    public bool respawn;

    private Vector3 originalPlayerScale;

    private CinemachineVirtualCamera cinemachineVirtualCamera;




   public  List <Enemy1> enemyRespawner;

    public List <Enemy1Ranged> enemyRangedRespawner;

    public List <FlyingEnemySelbstVersuch> flyingEnemySelbstVersuch;




    private void Start()
    {

        cinemachineVirtualCamera = GameObject.Find("OpenRoomMainCamera").GetComponent<CinemachineVirtualCamera>();

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerCombat = GameObject.Find("Player").GetComponent <PlayerCombatTry>();
        playerMove = GameObject.Find("Player").GetComponent<MovementPlayer>();
    }

    private void Update()
    {
        CheckRespawn();

        //respawnPoint.localScale = Vector3.one;
        
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        
        originalPlayerScale = playerStats.transform.localScale;
        respawn = true;
        //Time.timeScale = 0f;
        playerMove.canDash = true;
        playerCombat.isAttacking = false;
        //playerCombat.isAttacking = false;
        playerStats.gameObject.SetActive(false);
    }

    private void CheckRespawn()
    {
        
        



        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            foreach (Enemy1 enemy in enemyRespawner)
            {
                enemy.Respawn();
            }

            foreach (Enemy1Ranged enemy in enemyRangedRespawner)
            {
                enemy.Respawn();
            }

            foreach (FlyingEnemySelbstVersuch enemy in flyingEnemySelbstVersuch)
            {
                enemy.Respawn();
            }


            playerStats.gameObject.SetActive(true);
            // Instanziere das Prefab
            //GameObject playerClone = Instantiate(player, checkPointManager.GetLastCheckpointPosition(), Quaternion.identity);
            playerStats.transform.position = checkPointManager.GetLastCheckpointPosition();
            
            playerMove.IsGrappling = false;
            playerMove.isDashing = false;
            playerMove.canDash = true;
            playerMove.canGrapple = true;
            playerMove.didGrappleHit = false;
            playerCombat.isAttacking = false;
            playerStats.ResetHealth();
            

            // Stelle die ursprüngliche Skalierung des Spielers im Klon wieder her
            //playerClone.transform.localScale = originalPlayerScale;

            //Time.timeScale = 1f;
            cinemachineVirtualCamera.m_Follow = playerStats.transform;
            respawn = false;

        }

    }

   


}
