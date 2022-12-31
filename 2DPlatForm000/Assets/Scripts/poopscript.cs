using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopscript : MonoBehaviour
{
    [SerializeField]
    float speed;
    
    public int damage;
    [SerializeField]
    float timeToDestroy = 3f;

    public void StartShoot(bool isFacingLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (isFacingLeft)
        {
            rb2d.velocity = new Vector2(-speed, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 1);
        }

        Destroy(gameObject, timeToDestroy);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(damage);
        }
    }

}
