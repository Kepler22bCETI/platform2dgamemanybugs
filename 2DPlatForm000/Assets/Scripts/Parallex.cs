using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    [SerializeField] private Transform Cam;
    public float moveRate;
    private float startPointX;
    void Start()
    {
        startPointX = transform.position.x;
    }
    void Update()
    {
        transform.position = new Vector2(startPointX + Cam.position.x * moveRate, transform.position.y);
    }

}
