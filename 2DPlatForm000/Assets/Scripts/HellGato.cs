using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGato : Enemy
{
    public float speed;
    public float radius;
  
    private Transform playerTransform;


    public void Start()
    {

        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }
    public void Update()
    {
        base.Update();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }

        }
       
    }

 
}
