using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    private Transform playerTransform;

    private Vector3 direction;

    public void Initialize(Vector3 shootDirection, Transform player)
    {
        direction = shootDirection.normalized;
        playerTransform = player;
        Destroy(gameObject, lifetime); 
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Destroy(gameObject); 
        }
    }
}