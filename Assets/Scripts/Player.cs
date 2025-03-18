using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerID = 1; // 1 ou 2 pour différencier les joueurs

    public Animator animator;
    [Header("Stats")]
    public float health = 100;
    public float attack = 10;
    public float speed = 5;
    public float rotationSpeed = 100;
    public float fireRate = 1;
    public float projectileSpeed = 10;

    [Header("Controls")]
    public PlayerControls controls;
    private float nextFireTime = 0f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private void Start()
    {
        LoadControls();
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    public void LoadControls()
    {
        if (PlayerPrefs.HasKey($"Player{playerID}_MoveUp"))
        {
            controls.moveUp = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_MoveUp"));
            controls.moveDown = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_MoveDown"));
            controls.moveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_MoveLeft"));
            controls.moveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_MoveRight"));
            controls.fireNormal = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_FireNormal"));
            controls.fireSpecial = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString($"Player{playerID}_FireSpecial"));
        }
        else
        {
            SetDefaultControls();
        }
    }

    private void SetDefaultControls()
    {
        if (playerID == 1)
        {
            controls.moveUp = KeyCode.UpArrow;
            controls.moveDown = KeyCode.DownArrow;
            controls.moveLeft = KeyCode.LeftArrow;
            controls.moveRight = KeyCode.RightArrow;
            controls.fireNormal = KeyCode.Alpha1;
            controls.fireSpecial = KeyCode.Alpha2;
        }
        else if (playerID == 2)
        {
            controls.moveUp = KeyCode.W;
            controls.moveDown = KeyCode.S;
            controls.moveLeft = KeyCode.A;
            controls.moveRight = KeyCode.D;
            controls.fireNormal = KeyCode.E;
            controls.fireSpecial = KeyCode.A;
        }
    }

    public void SaveControls()
    {
        PlayerPrefs.SetString($"Player{playerID}_MoveUp", controls.moveUp.ToString());
        PlayerPrefs.SetString($"Player{playerID}_MoveDown", controls.moveDown.ToString());
        PlayerPrefs.SetString($"Player{playerID}_MoveLeft", controls.moveLeft.ToString());
        PlayerPrefs.SetString($"Player{playerID}_MoveRight", controls.moveRight.ToString());
        PlayerPrefs.SetString($"Player{playerID}_FireNormal", controls.fireNormal.ToString());
        PlayerPrefs.SetString($"Player{playerID}_FireSpecial", controls.fireSpecial.ToString());
        PlayerPrefs.Save();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector2 move = Vector2.zero;

        if (Input.GetKey(controls.moveUp)){
            moveDirection += Vector3.up;
            move.x = 1;
        };
        if (Input.GetKey(controls.moveDown)){
            moveDirection += Vector3.down;
            move.x = -1;
        };
        if (Input.GetKey(controls.moveLeft)){
            moveDirection += Vector3.left;
            move.y = -1;
        };
        if (Input.GetKey(controls.moveRight)) {
            moveDirection += Vector3.right;
            move.y = 1;
        };
        animator.SetFloat("Horizontal", move.y);
        animator.SetFloat("Vertical", move.x);
        animator.SetFloat("Speed", move.magnitude);

        transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(controls.fireNormal) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if (Input.GetKeyDown(controls.fireSpecial))
        {
            SpecialAttack();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * projectileSpeed;
    }

    private void SpecialAttack()
    {
        Debug.Log("Special attack triggered!");
    }
}

[System.Serializable]
public class PlayerControls
{
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode fireNormal;
    public KeyCode fireSpecial;
}