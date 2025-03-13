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
    public float deadzone;
    public Vector2 aimDirection;
    public bool gamepad;

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
    public bool grounded;//if on ground
    public bool laddered;//if touching ladder
    public float deadzone;//gamepad deadzone
    public bool gamepad;//if use gamepad
    public bool facingRight;
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
        if (!gamepad)//gamepad aimDir -> "Look"
        { aimDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); }
        gunHolder.lookAt(aimDirection);
        flipSprite(aimDirection.x > 0);//if need flip
        currentHealth = GetComponent<playerHealth>().currentHealth;
    }

    public void init(string skin, Vector3 pos, int health, playerAssets assetsRef, Vector2 initialDirection)
    {
        aimDirection = initialDirection;
        //set scheme
        if (GetComponent<PlayerInput>().currentControlScheme.Contains("pad"))
        {
            gamepad = true;
        }
        else
        {
            gamepad = false;
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("weapon"))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>(), true);
        }
        playerAssets = assetsRef;
        deadzone = .25f;//gamepad deadzone
        gameObject.GetComponentInChildren<bodyAnim>().init(skin, assetsRef);//skin
        gameObject.transform.position = pos;//spawn position
        gameObject.GetComponentInChildren<playerHealth>().init(200, assetsRef);//set health
        gunHolder = GetComponentInChildren<gunHolder>();
        gunHolder.bareHandsOffset();
    }
    public void flipSprite(bool right)//look left/right
    {
        //facing right
        if (right)
        {
            body.GetComponentInChildren<SpriteRenderer>().flipX = true;
            moveDirection(true);
        }
        //facing left
        else
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
    public void Look(InputAction.CallbackContext context)//GAMEPAD ONLY
    {
        if (gamepad && deadzoneCheck(context.ReadValue<Vector2>()[0], context.ReadValue<Vector2>()[1]))
        {
            aimDirection = context.ReadValue<Vector2>();
        }
    }
    private bool deadzoneCheck(float xInput, float yInput)//called by look
    {
        if (Mathf.Abs(xInput) > deadzone || Mathf.Abs(yInput) > deadzone)
        {
            return true;//out of deadzone
        }
        return false;//still in deadzone
    }
    //controls
    public void Move(InputAction.CallbackContext context)
    {
        //VALUES
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;


        //CLIMB
        if (context.ReadValue<Vector2>().y > 0 && laddered)
        {
            rb.velocity = new Vector2(rb.velocity.x, context.ReadValue<Vector2>().y * speed);
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