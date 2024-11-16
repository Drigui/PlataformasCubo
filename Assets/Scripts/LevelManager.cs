using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool hasKey;

    public GameObject winPanel;
    public GameObject canvas;

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
        Instance = this;


        timeCounter = 0f;
        hasKey = false;
        restartPoint = gameObject.transform.GetChild(0);
        player = GameObject.Find(GameConstant.PLAYER);
        _playerHealth = player.GetComponent<PlayerHealth>();
        player.transform.position = restartPoint.position;
        keysPoint = GameObject.Find(GameConstant.KEYS); //crear un empty KEYS/ITEMS que guarden ese tipo de objetos
        itemsPoint = GameObject.Find(GameConstant.ITEMS);

        foreach (Transform child in keysPoint.transform)
        {
            keyList.Add(child);
        }
        foreach (Transform child in itemsPoint.transform)
        {
            itemsList.Add(child);

        }

        if (SceneManager.GetActiveScene().name == GameConstant.LEVEL3_KEY)
        {
            winPanel = canvas.transform.GetChild(0).gameObject;
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
    public void ChangeScene(string scename)
    {
        SceneManager.LoadScene(scename);
    }
}
