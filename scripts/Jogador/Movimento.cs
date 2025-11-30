using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movimento : MonoBehaviour
{
    [Header("Inverter Eixos por Cena")]
    public bool inverterHorizontal = true;
    public bool inverterVertical = true;
    public bool trocarEixos = false;


    [Header("Movimento e Rotacao")]
    private Rigidbody rb;
    private float MoveSpeed;
    public float walkSpeed;
    public float mirandoSpeed;

    private float moveHorizontal;
    private float moveForward;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Animator anim;

    private float idleTimer = 0f;
    public float idleThreshold = 5f; 

    

    [Header("KeyCode")]
    public KeyCode puloKey = KeyCode.Keypad0;
    public KeyCode agachar = KeyCode.LeftControl;
    public KeyCode agachar2 = KeyCode.RightControl;

    [Header("Pulo e Gravidade")]
    public float jumpForce = 12f;
    public float fallMultiplier = 2.5f; 
    public float ascendMultiplier = 2f; 
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;

    [Header("Agachar")]
    public float agacharVelocidade;
    public bool isCrouching;
    private float startYScale;

    public bool isJumping;
    public MovementState state;

    public GameObject Lira;

    public enum MovementState
    {
        andar,
        mirando,
        agachar,
        ar
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();

        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        isCrouching = false;

        string cena = SceneManager.GetActiveScene().name;

        startYScale = transform.localScale.y;

        if (cena == "cena01")
        {
            inverterHorizontal = false;   
            inverterVertical = false;   
        }
        if (cena == "cCena02")
        {
            trocarEixos = true;
            inverterHorizontal = false;   
            inverterVertical = true;    
        }

    }

    void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        if (trocarEixos)
        {
            moveHorizontal = inputV * (inverterHorizontal ? -1 : 1);
            moveForward = inputH * (inverterVertical ? -1 : 1);
        }
        else
        {
            moveHorizontal = inputH * (inverterHorizontal ? -1 : 1);
            moveForward = inputV * (inverterVertical ? -1 : 1);
        }

        anim.SetBool("Pulo", false);


        if (Input.GetButtonDown("Jump") && isGrounded || Input.GetKeyDown(puloKey) && isGrounded)
        {
            Jump();
        }

        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(agachar) || Input.GetKeyDown(agachar2))
        {
            
            isCrouching = true;
            anim.SetBool("Agachado", true);

            CapsuleCollider col = GetComponent<CapsuleCollider>();
            col.height = 1.5f;
        }

        if (Input.GetKeyUp(agachar) || Input.GetKeyUp(agachar2))
        {
           
            isCrouching = false;
            anim.SetBool("Agachado", false);
            CapsuleCollider col = GetComponent<CapsuleCollider>();
            col.height = 2;
        }

        HandleIdleSpecial();
    }

    void FixedUpdate()
    {
        MovePlayer();
        StateHandler();
        ApplyJumpPhysics();
    }

    private void StateHandler()
    {
        if (Input.GetKey(agachar) || Input.GetKey(agachar2))
        {
            state = MovementState.agachar;
            MoveSpeed = agacharVelocidade;
        }
        else if (isGrounded && Input.GetMouseButton(1) || isGrounded && Input.GetMouseButton(1))
        {
            state = MovementState.mirando;
            MoveSpeed = mirandoSpeed;
        }
        else if (isGrounded)
        {
            state = MovementState.andar;
            MoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.ar;
        }
    }

    void HandleIdleSpecial()
    {
        bool parado = moveHorizontal == 0 && moveForward == 0;

        bool emPe = !isCrouching && !Input.GetMouseButton(1) && isGrounded && !Input.GetMouseButton(0);

        if (parado && emPe)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0f; 
        }


        anim.SetFloat("IdleTime", idleTimer);

        
    }


    void MovePlayer()
    {
        anim.SetFloat("Andar", new Vector2(moveHorizontal,moveForward).magnitude);
        Vector3 movement= new Vector3(moveHorizontal, 0, moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

    }

    void Jump()
    {
        isJumping = true;
        anim.SetBool("Pulo", true);
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); 
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } 
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

}