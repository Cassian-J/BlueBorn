using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystic : Player
{
    [Header("Player Settings")]
    public int playerID = 1; // 1 ou 2 pour différencier les joueurs
    public GameObject _hammer;
    public GameObject _bulletPrefab;
    public Animator animator;
    public Transform firePoint;

    [Header("Stats")]
    public float health = 100;
    public float attack = 10;
    public float speed = 2;
    public float rotationSpeed = 100;
    public float fireRate = 5;
    public float projectileSpeed = 10;

    private float nextFireTime = 0f;

    private void Update()
    {
        HandleJoystickInput();
    }

    private void HandleJoystickInput()
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
        }

        if (Input.GetButtonDown($"isAttacking"))
        {
            MeleeAttack();
        }
    }

    private void Shoot(Vector3 vector)
    {
        animator.SetBool("isShooting", true);
        GameObject bullet = Instantiate(_bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.playerID = playerID;
            bulletScript.speed = projectileSpeed;
            bulletScript.move = vector;
        }
    }

    private void MeleeAttack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
