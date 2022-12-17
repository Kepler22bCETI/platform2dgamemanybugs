using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform respawnPoint;
    


    void Update()
    {
        
        PlayerHealth player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (player.health <= 0)
        {
            playerTransform.transform.position = respawnPoint.transform.position;
            player.health = 15;
            Physics.SyncTransforms();
        }
    }

}
