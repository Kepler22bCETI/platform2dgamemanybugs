using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGato : Enemy
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    private void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        base.Update();
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (disToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
