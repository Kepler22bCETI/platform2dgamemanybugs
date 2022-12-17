using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashTime;
    private PlayerHealth playerHealth;
    public GameObject dropCoin;
    protected AudioSource deathAudio;
    

    //protected Animator enemyDeathAnim;
    protected virtual void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        deathAudio = GetComponent<AudioSource>();
        //enemyDeathAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        //GhostHalo ghosthalo = gameObject.GetComponent<GhostHalo>();
        //Skeleton skeleton = gameObject.GetComponent<Skeleton>();

    }
    protected void Update()
    {
        if (health <= 0)
        {
  
            Instantiate(dropCoin, transform.position, Quaternion.identity);
            
            Death();

        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        deathAudio.Play();
        FlashColor(flashTime);

    }
    void FlashColor(float time)
    {
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
                
            }            
        }
    }
    
    
    //protected void DieAnim()
    //{
    //    enemyDeathAnim.SetTrigger("death");
    //}
 

    public void Death()
    {
        Destroy(gameObject);
    }


}