using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Rigidbody2D rb;
    private GameObject playerFeet; // for ground check
    private GameObject playerWall; // for wall check
    private PlayerAnimations _playerAnimations;


    [Header("InputAction")]
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;

    //doublejump/ leaveplatform
    [SerializeField]private bool doubleJump;
    [SerializeField] private bool leavePlatform;

    //cancelar mov A/D teclado
    public float cancelD;
    public float cancelA;

    //check layer
    public LayerMask Layer; //todas las layers que permiten salto, se marcan en el inspector




    //Movement
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;



    //coyote time
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;

    //[Header("Jump Buffer")]
    //[SerializeField] private float jumpBuffer = 0.2f;
    //[SerializeField] private float jumpBufferCounter;


    //inputs
    private Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }

    //dash
    private bool canDash = true;
    private bool isDashing;
    [Header("Dashing")]
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    [SerializeField] private TrailRenderer trailRenderer;


    


    #region Enable/Disable
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        jump = playerControls.Player.Jump;
        //jump.Enable();

    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();

    }
    #endregion
    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        playerFeet = GameObject.Find("PlayerFeet");
        playerWall = GameObject.Find("PlayerWall");
        trailRenderer = GetComponent<TrailRenderer>();
        rb.gravityScale = gravity;

    }
    private void Update()
    {
        GetInputs();
        CoyoteTime();
        if (coyoteTimeCounter < -0.1 && leavePlatform == true)
        {
            doubleJump = true;
        }
        if (isDashing)
        {
            return;
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        MovePlayer(); //al ser por velocity(Physics) va en fixed

    }
    #region Get/Cancel Inputs

    /// <summary>
    /// coge direccion mediante input system con vector 2
    /// </summary>
    private void GetInputs()
    {
        CancelInputs();
        
        ///si Ambas teclas estan pulsadas coge el movimento de la ultima tecla pulsada comparando valores
        if (Keyboard.current.dKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            if (cancelD < cancelA)
            {
                moveDirection.x = move.ReadValue<Vector2>().x + 1; // Mantén dirección izquierda si A aún está presionada.
            }
            else
            {
                moveDirection.x = move.ReadValue<Vector2>().x - 1;
            }
        }
        else
        {
            moveDirection = move.ReadValue<Vector2>();
        }
    }

    /// <summary>
    /// suma deltaTime para comparar valores y no cancelar el movimiento al tener dos teclas pulsadas. release devuelve 0
    /// </summary>
    private void CancelInputs()
    {
        if (Keyboard.current.dKey.isPressed)
        {
            cancelD += Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            cancelA += Time.deltaTime;
        }
        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            cancelD = 0;
        }
        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            cancelA = 0;
        }
    }
    #endregion

    /// <summary>
    /// Change player position with axis and speed
    /// </summary>
    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        Debug.Log(_playerAnimations.colorChange);
    }

    #region Checkers
    /// <summary>
    /// Check if player is on the ground an returns if its grounded
    /// </summary>
    /// <returns> grunded state in bool</returns>
    public bool IsGrounded()
    {
        //variables de los 3 rayos para el return
        RaycastHit2D physics;
        RaycastHit2D physicsR;
        RaycastHit2D physicsL;
       
            Debug.DrawRay(playerFeet.transform.position, Vector3.down * 0.1f, Color.red);
            Debug.DrawRay(playerFeet.transform.position + new Vector3(0.5f, 0, 0), Vector3.down * 0.1f, Color.red);
            Debug.DrawRay(playerFeet.transform.position + new Vector3(-0.5f, 0, 0), Vector3.down * 0.1f, Color.red);

            physics = Physics2D.Raycast(playerFeet.transform.position, Vector3.down, 0.1f, Layer);
            physicsR = Physics2D.Raycast(playerFeet.transform.position + new Vector3(0.5f, 0, 0), Vector3.down, 0.1f, Layer);
            physicsL = Physics2D.Raycast(playerFeet.transform.position + new Vector3(-0.5f, 0, 0), Vector3.down, 0.1f, Layer);
            return physics || physicsR || physicsL;
        
    }
    /// <summary>
    /// detects all directions. use for wall detect
    /// </summary>
    /// <returns></returns>
    public bool IsWall()
    {
        if (moveDirection.y < 0.60f && moveDirection.y > -0.60f ) //condition to only lerft/right detect
        {
            Debug.DrawRay(playerWall.transform.position, moveDirection * 0.75f, Color.red);
            return Physics2D.Raycast(playerWall.transform.position, moveDirection, 0.75f, Layer);
        }
        return false;
    }
    #endregion

   public void DashAction(InputAction.CallbackContext context)
    {
        if (canDash && context.performed)
        {
            StartCoroutine(Dash());
        Debug.Log(rb.velocity);

        }
    }


    #region Jump
    /// <summary>
    /// Jump movement
    /// </summary>
    public void Jump(InputAction.CallbackContext context)
    {
        //jump if is groun/wall +long jump
        if (context.started)//si pulsa boton salto
        {
            if (!IsWall())
            {
                _playerAnimations.JumpAnimation();
            }
            if (coyoteTimeCounter > 0 || doubleJump) //salto normal o largo en el suelo, se ha sustituido por el coyote time
            {
                if (IsWall())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 32);

                }
                else
                {
                    doubleJump = !doubleJump;
                    leavePlatform = false;
                    //rb.gravityScale = gravity;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    //StartCoroutine(WaitGravity());
                }

            }
        }

        // esto permite hacer el salto corto
        if (context.canceled && rb.velocity.y > 0f)
        {
            //divide la velocidad del salto cuando liberes el boton salto a la mitad de la velocidad que lleva
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);
            coyoteTimeCounter = 0f;
        }
    }
    #endregion

    /// <summary>
    /// para cancelar el impulso que mantiene si saltas y te pegas a la pared/ resetear el doble salto
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsWall() && !IsGrounded())
        {
            //rb.gravityScale = gravity / 4;
        }
        if (IsGrounded())
        {
            doubleJump = false;
        }
    }

    /// <summary>
    /// coroutine reset jump after leaving platformw
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionExit2D(Collision2D collision)
    {
            leavePlatform = true;
    }

    private void CoyoteTime()
    {
        if (IsGrounded() || IsWall())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            //rb.gravityScale = gravity;
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// wait coyotetime seconds to set jump to true and allow 1 jump
    /// </summary>
    /// <returns></returns>
    IEnumerator SetJump()
    {
        yield return new WaitForSeconds(0.16f);
        doubleJump = true;
    }

    private void DashType()
    {
        if (moveDirection.x == 0f && moveDirection.y == 0f)
        {
            if (_playerAnimations.PlayerSprite.flipX)
            {
                rb.velocity = new Vector2(-dashingPower, 0);
            }
            else
            {
                rb.velocity = new Vector2(dashingPower, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * dashingPower, moveDirection.y * dashingPower);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        DashType();
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator WaitGravity()
    {
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = gravity / 4;
    }

}
