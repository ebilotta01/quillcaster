//
// CursorHandler.cs
// Developers: Evan Bilotta
//
// This script is responsible for controlling the player's movement and animations.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;

    private Vector2 move = Vector2.zero;
    public bool castingAbility;
    private Rigidbody2D rb;
    public GameObject player;
    public int damage = 25;
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;
    public Animator anim;
    public void OnEnable() {
        moveAction.Enable();
    }
    public void OnDisable() {
        moveAction.Disable();
    }

    void Start() 
    {
        castingAbility = false;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        if(move.x != 0 || move.y != 0) {
            anim.SetFloat("xPos", move.x);
            anim.SetFloat("yPos", move.y);
            anim.SetBool("isWalking", true);
        }
        else {
            anim.SetBool("isWalking", false);
        }
    }
    
    private void FixedUpdate() {
        Move();
    }
    public void Move() {
        rb.velocity = new Vector2(move.x * PlayerStats.speed, move.y * PlayerStats.speed);
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
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }    
    

}
