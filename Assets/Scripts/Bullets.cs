using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public int playerID;
    public float speed;
    public Vector3 move;
    private void Update()
    {
        HandleAction(speed,move);
    }
    private void HandleAction(float bulletSpeed, Vector3 movement)
    {
        transform.Translate(movement.normalized * bulletSpeed * Time.deltaTime);
    }
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if (collision.CompareTag("Player")){
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.playerID != playerID)
            {
                player.TakeDamage(player.attack);
                Destroy(gameObject);
            }

    } else if (!collision.CompareTag("bullet")) {
        Destroy(gameObject);
    }
   }

}