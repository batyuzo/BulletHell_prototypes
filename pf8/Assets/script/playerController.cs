using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [Header("Player Component Reference")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] float coyote;
    [SerializeField] int defaultHealth;
    public int fps;
    [SerializeField] Transform flip;
    [SerializeField] Transform gunHold;
    [SerializeField] Transform head;
    [SerializeField] Transform body;
    public GameObject assetHandler;

    [Header("PlayerLogs")]
    public int jumpLeft;
    public float coyoteCount;
    public float jumpBuffer;
    public float horizontal;
    public bool moving;
    public bool grounded;
    public int maxHealth;
    public int currentHealth;
    public bool forwardMotion;






    private void FixedUpdate()
    {
        //!----------MOVEMENT----------!
        //moves get executed per physics update
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //coyote decreases mid-air
        if (coyoteCount > 0 && !groundCheckP1.grounded) { coyoteCount -= 0.2f; }
        //buffer decreases mid-air
        if (jumpBuffer > 0) { jumpBuffer -= 0.50f; }




        grounded = groundCheckP1.grounded;
    }

    private void Awake()
    {
        //ignore weapon collision
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("weapon"))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>(), true);
        }

        //set assetHandler reference
        assetHandler = GameObject.FindGameObjectWithTag("playerAssets");

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //buffered jump on landing
        if (groundCheckP1.grounded && jumpBuffer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        currentHealth = gameObject.GetComponent<playerHealth>().currentHealth;

        //if theres a weapon equippable



    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        if (context.performed) { moving = true; }
        else { moving = false; }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && groundCheckP1.grounded)
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
        if (context.performed && scanCheck() != null)
        {
            gameObject.GetComponentInChildren<gunHolder>().Equip();
        }

    }

    public void ChangeSkin(InputAction.CallbackContext context)
    {
        if (context.performed) { gameObject.GetComponentInChildren<bodyAnim>().skinSwitch("butcher"); }
    }


    public void Drop(InputAction.CallbackContext context)
    {
        Debug.Log(gameObject.name + " dropped");
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

    private GameObject scanCheck()
    {
        GameObject obj = gameObject.GetComponentInChildren<scanner>().getEquippable();

        if (obj != null)
        {
            Debug.Log("scanCheck true");
            return obj;
        }
        else
        {
            Debug.Log("scanCheck false");
            return null;
        }
    }

}


