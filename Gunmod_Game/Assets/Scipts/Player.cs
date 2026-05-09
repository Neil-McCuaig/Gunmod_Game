using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Based on this tutorial;
    //https://www.youtube.com/watch?v=f473C43s8nE

    [Header("Movement")]
    public float movementSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    GameManager manager;

    public bool playerActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        playerActive = false;

        manager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (playerActive == true)
        {
            MyInput();
            SpeedControl();
        }


        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(pauseKey))
        {
            Debug.Log("You pressed the escape key.");
            if (manager.pauseMode == false)
            {
                manager.pauseMenuEnable();
            }
            else if (manager.pauseMode == true)
            {
                manager.pauseMenuDisable();
            }
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("We are in the beam.");
        if (other.gameObject.CompareTag("EndLevel") && manager.boughtTheMilk == true && manager.hasGreenJogHurt == true && manager.hasSanta == true && manager.hasWindex == true && manager.hasApple == true)
        {
            Debug.Log("Going uuuuuuupppppppp!!!");
            manager.victory = true;
        }
        if (other.gameObject.CompareTag("BuyMilk"))
        {
            Debug.Log("Mooove it!");
            manager.boughtTheMilk = true;
        }
        else if (!other.gameObject.CompareTag("EndLevel"))
        {
            Debug.Log("Ouch");
        }
        //if (other.CompareTag("DriveZones"))
        //if (other.TryGetComponent<Car_Crash_Logic>(out Car_Crash_Logic crash_Logic))
        //{
            //if (crash_Logic.violenceTime == true)
            //{
                //manager.defeat = true;
            //}
        //}
    }
}
