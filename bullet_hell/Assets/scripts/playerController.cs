using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    [Header("Player Component Reference")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] float coyote;
    public int fps;
    [SerializeField] Transform flip;
    [SerializeField] Transform head;
    [SerializeField] Transform body;
    public groundCheck groundCheck;

    [Header("script refs")]
    public gunHolder gunHolder;
    public playerAssets playerAssets;

    [Header("PlayerLogs")]
    public int jumpLeft;
    public float coyoteCount;
    public float jumpBuffer;
    public float horizontal;
    public float vertical;
    public bool moving;
    public int maxHealth;
    public int currentHealth;
    public bool forwardMotion;
    public bool grounded;
    public bool laddered;
    private void FixedUpdate()
    {
        //!----------MOVEMENT----------!
        //moves get executed per physics update
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //coyote decreases mid-air
        if (coyoteCount > 0 && !grounded) { coyoteCount -= 0.2f; }
        //buffer decreases mid-air
        grounded = groundCheck.getGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        currentHealth = GetComponent<playerHealth>().currentHealth;
    }

    public void init(string skin, Vector3 pos, int health, playerAssets assetsRef, string schemeName)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("weapon"))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>(), true);
        }
        playerAssets = assetsRef;

        gameObject.GetComponentInChildren<bodyAnim>().init(skin, assetsRef);//skin
        gameObject.transform.position = pos;//spawn position
        gameObject.GetComponentInChildren<playerHealth>().init(200, assetsRef);//set health
        gameObject.GetComponentInChildren<gunHolder>().init(schemeName);//gamepad or not?
        gameObject.GetComponentInChildren<gunHolder>().bareHandsOffset();
    }
    public void Flip()
    {

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //facing right
        if (mouseWorldPos.x > transform.position.x)
        {
            body.GetComponentInChildren<SpriteRenderer>().flipX = true;
            moveDirection(true);
        }
        //facing left
        if (mouseWorldPos.x < transform.position.x)
        {
            body.GetComponentInChildren<SpriteRenderer>().flipX = false;
            moveDirection(false);
        }
    }
    private void moveDirection(bool lookingRight)
    {
        //if looks right and goes right -> forward motion
        if (lookingRight && horizontal > 0) { forwardMotion = true; }
        //if looks left and goes right -> backwards motion
        else if (!lookingRight && horizontal > 0) { forwardMotion = false; }
        //if looks left and goes left -> forward motion
        else if (!lookingRight && horizontal < 0) { forwardMotion = true; }
        //if looks right and goes left -> backwards motion
        else if (lookingRight && horizontal < 0) { forwardMotion = false; }
    }

    //controls
    public void Move(InputAction.CallbackContext context)
    {
        //VALUES
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;

        //CLIMB
        if (vertical != 0 && laddered)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }

        //WALK
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //ANIM
        if (context.performed && (horizontal != 0 || vertical != 0))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else jumpBuffer = 1f;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponentInChildren<gunHolder>().Fire();
        }
    }
    public void AltFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponentInChildren<gunHolder>().AltFire();
        }
    }
    public void Equip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gunHolder.Equip();
        }

    }
    public void Drop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponentInChildren<gunHolder>().Drop();
        }
    }
    //ladders and deaths
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("death"))
        {
            GetComponent<playerHealth>().death();
        }
        else if (collision.CompareTag("ladder"))
        {
            laddered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            laddered = false;
        }
    }
}