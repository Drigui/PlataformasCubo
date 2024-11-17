using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Hud Texts")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI deathCounterText;
    public GameObject keyIcon;


    [Header("Timer")]
    //LevelTimer
    private int minutes = 0;
    private int seconds = 0;

    private void Awake()
    {
        Instance = this;
        keyIcon.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)LevelManager.Instance.TimeCounter % 60;
        minutes = (int)LevelManager.Instance.TimeCounter / 60;
    }

    private void OnGUI()
    {
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        pointsText.text = LevelManager.Instance.pickUpPointsTotal.ToString("0000");
        deathCounterText.text = LevelManager.Instance.DeathCounter.ToString("0000");

    }
}
