using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.PLAYER))
        {
            LevelManager.Instance.hasKey = true;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
