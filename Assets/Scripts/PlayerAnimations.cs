using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator playerAnimator;
    private SpriteRenderer playerSprite;



    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnimations();
        FlipSprite();
    }

    /// <summary>
    /// change animation in order to action
    /// </summary>
    private void ChangeAnimations()
    {
        playerAnimator.SetFloat("X", _playerMovement.MoveDirection.magnitude);
        playerAnimator.SetBool("Climb", false);
        if (_playerMovement.IsWall())
        {
            playerAnimator.SetBool("Climb", true);
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

    #region ChangeColor
    public void ChangeBlack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerAnimator.SetLayerWeight(1, 1);
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
    public void ChangeWhite(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerAnimator.SetLayerWeight(1, 0);
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
    #endregion

    public void JumpAnimation()
    {
        playerAnimator.SetTrigger("Jump");
    }

}
