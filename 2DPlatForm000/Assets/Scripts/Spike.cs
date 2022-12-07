using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth;
    
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            playerHealth.DamagePlayer(damage);
        }
    }
}
