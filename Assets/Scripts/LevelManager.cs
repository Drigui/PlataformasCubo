using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// lvl manager controla puntos cada nivel, puedeEntrarPuerta?, cambio escena, HUD otro script,game over, win
    /// </summary>

    public static LevelManager Instance;

    [SerializeField]private Transform restartPoint;
    [SerializeField]private GameObject player;
    private void Awake()
    {
        Instance = this;
        restartPoint = gameObject.transform.GetChild(0);
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Death()//es el game over tipo meat boy
    {
        player.transform.position = restartPoint.position;
    }
}
