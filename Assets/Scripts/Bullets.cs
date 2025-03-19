using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public int playerID;
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