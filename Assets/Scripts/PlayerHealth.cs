using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private int health;
    private PlayerAnimations playerAnimations;
    public int Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage()
    {
        health--;
        if (health < 0)
        {
            LevelManager.Instance.Death();
            LevelManager.Instance.ResetValues();

        }
    }
}
