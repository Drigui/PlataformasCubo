using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator playerAnimator;
    private SpriteRenderer playerSprite;
    public int colorChange;

    [SerializeField] private Enemy _enemy;
    public SpriteRenderer PlayerSprite { get => playerSprite; set => playerSprite = value; }

    private void Awake()
    {
        colorChange = 0;
        _playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        if (GameObject.FindGameObjectWithTag(GameConstant.ENEMY) != null)
        {
            _enemy = GameObject.FindGameObjectWithTag(GameConstant.ENEMY).GetComponent<Enemy>();

        }


    }
    
    // Update is called once per frame
    void Update()
    {
        ChangeAnimations();
        FlipSprite();
        ActualizarLayer();
    }

    /// <summary>
    /// change animation in order to action
    /// </summary>
    private void ChangeAnimations()
    {
        playerAnimator.SetFloat(GameConstant.X, _playerMovement.MoveDirection.magnitude);
        playerAnimator.SetBool(GameConstant.CLIMB, false);
        if (_playerMovement.IsWall())
        {
            playerAnimator.SetBool(GameConstant.CLIMB, true);
        }
    }
    /// <summary>
    /// change sprite by flipping it
    /// </summary>
    private void FlipSprite()
    {
        if (_playerMovement.MoveDirection.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if (_playerMovement.MoveDirection.x < 0)
        {
            playerSprite.flipX = true;
        }
    }

    private void ActualizarLayer()
    {
        playerAnimator.SetLayerWeight(1, colorChange);
    }

    #region ChangeColor
    public void ChangeBlack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            colorChange = 1;
            playerAnimator.SetLayerWeight(1, colorChange);
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
    public void ChangeWhite(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            colorChange = 0;
            playerAnimator.SetLayerWeight(1, colorChange);
            playerSprite = GetComponentInChildren<SpriteRenderer>();

        }
    }
    #endregion

    public void JumpAnimation()
    {
        playerAnimator.SetTrigger(GameConstant.JUMP);
    }

}
