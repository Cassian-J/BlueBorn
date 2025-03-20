using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystic : Player
{
    
    private void HandleAction()
    {
        float moveX = Input.GetAxis($"Horizontal");
        float moveY = Input.GetAxis($"Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0);
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime);

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", moveDirection.magnitude);

        if (Input.GetButtonDown($"isShooting"))
        {
            Shoot(moveDirection);
            nextFireTime = Time.time + 1 / fireRate;
        }

        if (Input.GetButtonDown($"isAttacking"))
        {
            MeleeAttack();
        }
    }

}
