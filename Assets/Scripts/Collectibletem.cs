using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField]private int pickUpPoints;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.PLAYER))
        {
            LevelManager.Instance.pickUpPointsTotal += pickUpPoints;
            gameObject.SetActive(false);
        }
    }
}
