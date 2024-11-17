using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerAnimations _playerAnimations;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        _playerAnimations = GameObject.Find(GameConstant.PLAYER).GetComponent<PlayerAnimations>();
        playerHealth = GameObject.Find(GameConstant.PLAYER).GetComponent<PlayerHealth>();
    }

  
    /// <summary>
    /// death if color
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYER))
        {
            ///collision White
            if (gameObject.CompareTag(GameConstant.WHITE))
            {
                if (_playerAnimations.colorChange == 1)
                {
                    playerHealth.TakeDamage();
                    Debug.Log(collision.gameObject.GetComponent<PlayerHealth>());
                    _playerAnimations.colorChange = 0;
                }
                else
                {
                    return;
                }
            }

            ///collision black
            else if (gameObject.CompareTag(GameConstant.BLACK))
            {
                if (_playerAnimations.colorChange == 0)
                {
                    playerHealth.TakeDamage();
                    _playerAnimations.colorChange = 0;

                }
                else
                {
                    return;
                }
            }
        }
    }


    /// <summary>
    /// death if enemy
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYER))
        {
            if (gameObject.CompareTag(GameConstant.ENEMY))
            {
                playerHealth.TakeDamage();
                _playerAnimations.colorChange = 0;
            }
        }
    }
    

}
