using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float damage = 20f;
    public int playerID;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("27");
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player != null && player.playerID != playerID)
            {   
                player.TakeDamage(player.attack*2); 
                Destroy(gameObject);
            }
        }
    }
}
