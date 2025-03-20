using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerID = 1; // 1 ou 2 pour différencier les joueurs
    public GameObject _hammer;
    public GameObject _bulletPrefab;
    public Animator animator;   
    public Collider2D hammerCollider;
    [Header("Stats")]
    public float health = 100;
    public float attack = 10;
    public float speed = 2;
    public float rotationSpeed = 100;
    public float fireRate = 5;
    public float projectileSpeed = 10;
    
    
    [Header("Controls")]
    public PlayerControls controls;
    private float nextFireTime = 0f;
    public Transform firePoint;
    


    private void Start()
    {
        LoadControls();

        if (_hammer != null)
        {
            hammerCollider = _hammer.GetComponent<Collider2D>();
            if (hammerCollider == null)
            {
                Debug.LogError("Aucun Collider2D trouvé sur le marteau !");
            }
            else
            {
                hammerCollider.enabled = false;
            }
        }
    }

    private void Update()
    {
        HandleAction();
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
            controls.fireNormal = KeyCode.Keypad1;
            controls.fireSpecial = KeyCode.Keypad2;
        }
        else if (playerID == 2)
        {
            controls.moveUp = KeyCode.W;
            controls.moveDown = KeyCode.S;
            controls.moveLeft = KeyCode.A;
            controls.moveRight = KeyCode.D;
            controls.fireNormal = KeyCode.E;
            controls.fireSpecial = KeyCode.Q;
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

    private void HandleAction()
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

    if (Input.GetKey(controls.fireNormal) && Time.time >= nextFireTime && moveDirection != Vector3.zero)
    {
        Shoot(moveDirection);
        
        nextFireTime = Time.time + 1 / fireRate;
    }
    else if (Input.GetKeyUp(controls.fireNormal))
    {
        animator.SetBool("isShooting", false);
    }

    if (Input.GetKeyDown(controls.fireSpecial))
    {
        MeleeAttack();
        
    }else if (Input.GetKeyUp(controls.fireSpecial))
    {
        animator.SetBool("isAttacking", false);
    }

    transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
}

    private void Shoot(Vector3 vector)
    {   
        animator.SetBool("isShooting", true);
        Debug.Log("player " + playerID + " Attack triggered!");
        GameObject bullet = Instantiate(_bulletPrefab, firePoint.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.playerID = playerID;  
            bulletScript.speed = speed*2;
            bulletScript.move = vector;
        }
        
    }

    private void MeleeAttack()
    {
        animator.SetBool("isAttacking", true);
        Debug.Log("player "+playerID +" Special attack triggered!");
        
        }
        
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("player "+playerID +" took " + damage + " damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("player "+playerID +" died!");
        Destroy(gameObject);
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


