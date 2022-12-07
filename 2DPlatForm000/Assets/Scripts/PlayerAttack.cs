using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    private Animator animator;
    private PolygonCollider2D collider2D;

    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {            
            animator.SetTrigger("IsAttacking");
            StartCoroutine(startAttack());
            
        }
    }
    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(damage);
        }
    }
}
