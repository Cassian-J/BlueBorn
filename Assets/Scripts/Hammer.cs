using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float damage = 20f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Debug.Log("player "+player.playerID +" took " + damage + " damage! Remaining health: " + (player.health-damage));

            if (player != null)
            {
                player.TakeDamage(damage); 
            }
        }
    }
}

