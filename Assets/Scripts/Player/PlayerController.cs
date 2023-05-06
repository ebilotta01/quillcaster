using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    public float speed = Player.speed;
    public InputAction playerActions;
    private Vector2 move = Vector2.zero;
    private Rigidbody2D rb;
    public GameObject player;
    public int damage = 25;
    public int currentHealth;
    public int maxHealth = 100;

    public HealthBar healthBar;
    public Animator anim;

    private void OnEnable() {
        playerActions.Enable();
    }
    private void OnDisable() {
        playerActions.Disable();
    }

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        move = playerActions.ReadValue<Vector2>();
        
        if(move.x != 0 || move.y != 0) {
            anim.SetFloat("xPos", move.x);
            anim.SetFloat("yPos", move.y);
            anim.SetBool("isWalking", true);
        }
        else {
            anim.SetBool("isWalking", false);
        }
        //this.transform.Translate(move * Player.speed * Time.deltaTime);
    }
    
    private void FixedUpdate() {
        rb.velocity = new Vector2(move.x * speed, move.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth -= damage;
            Debug.Log("Player health: " + maxHealth);

            if (maxHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
