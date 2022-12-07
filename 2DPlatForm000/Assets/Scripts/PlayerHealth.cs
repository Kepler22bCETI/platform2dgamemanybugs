using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int blinks;
    public float time;
    public float hitBoxCdTime;
    private Renderer myRender;
    private PolygonCollider2D polygonCollider2D;

    void Start()
    {
        myRender = GetComponent<Renderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }


    public void DamagePlayer(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }                    
        BlinkPlayer(blinks, time);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }
    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }
    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
    
}
