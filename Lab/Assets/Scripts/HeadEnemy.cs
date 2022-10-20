using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    private Enemy parentEnemy;
    private void OnEnable()
    {
        if(parentEnemy == null)
        {
            parentEnemy = GetComponentInParent<Enemy>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Foot"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if(player != null)
            {
                Rigidbody2D rg = player.gameObject.GetComponent<Rigidbody2D>();
                rg.velocity = new Vector2(rg.velocity.x, 5);
                
            }
            parentEnemy.Hit();
        }
    }
    
}
