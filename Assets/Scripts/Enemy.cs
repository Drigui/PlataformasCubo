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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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

            ///collision White
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
