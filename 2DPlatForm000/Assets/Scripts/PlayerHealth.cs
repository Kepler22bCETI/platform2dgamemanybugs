using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int blinks;
    public float time;
    public float hitBoxCdTime;
    private Renderer myRender;
    private PolygonCollider2D polygonCollider2D;

    [SerializeField] private AudioSource hurtAudio;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fill;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform respawnPoint;
    void Start()
    {
        myRender = GetComponent<Renderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        healthSlider.value = healthSlider.maxValue = health;
        fill.color = Color.green;
    }
    void Update()
    {
        if (health < 20)
        {
            fill.color = Color.yellow;
        }
        if (health < 10)
        {
            fill.color = Color.red;
        }
        if (health <= 0)
        {
            playerTransform.transform.position = respawnPoint.transform.position;
            health = 25;
            Physics.SyncTransforms();
            healthSlider.value = healthSlider.maxValue = health;
            fill.color = Color.green;
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        hurtAudio.Play();
        healthSlider.value = health;

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
