using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool hasKey;
    /// <summary>
    /// lvl manager controla puntos cada nivel, puedeEntrarPuerta?, cambio escena, HUD otro script,game over, win
    /// </summary>

    public static LevelManager Instance;

    [SerializeField] private List<Transform> keyList = new List<Transform>();
    [SerializeField] private List<Transform> itemsList = new List<Transform>();
    [SerializeField] private GameObject keysPoint;
    [SerializeField] private GameObject itemsPoint;

    [SerializeField] private float timeCounter;


    [SerializeField]private Transform restartPoint;
    [SerializeField]private GameObject player;

    public bool pickKey;
    public int pickUpPointsTotal;
    private PlayerHealth _playerHealth;
    private PlayerAnimations _playerAnimations;
    [SerializeField] private int deathCounter;


    private void Awake()
    {
        timeCounter = 0f;
        hasKey = false;
        Instance = this;
        restartPoint = gameObject.transform.GetChild(0);
        player = GameObject.Find(GameConstant.PLAYER);
        _playerHealth = player.GetComponent<PlayerHealth>();
        player.transform.position = restartPoint.position;

        foreach (Transform child in keysPoint.transform)
        {
            keyList.Add(child);
        }
        foreach (Transform child in itemsPoint.transform)
        {
            itemsList.Add(child);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
    }
    public void Death()//es el game over tipo meat boy
    {
        player.transform.position = restartPoint.position;
        deathCounter = Mathf.Abs(_playerHealth.Health);

    }

    public void ResetValues()
    {
        pickUpPointsTotal = 0;
        
            foreach (Transform item in keyList)
            {
                item.gameObject.SetActive(true);

            }foreach (Transform item in itemsList)
            {
                item.gameObject.SetActive(true);

            }
        
       
        
    }
}
