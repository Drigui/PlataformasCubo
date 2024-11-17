using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneName;
    public GameObject backMenuBt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //load current scene +1
        if (LevelManager.Instance.hasKey && SceneManager.GetActiveScene().buildIndex < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else if (LevelManager.Instance.hasKey && SceneManager.GetActiveScene().name == GameConstant.LEVEL3_KEY)
        {
            LevelManager.Instance.winPanel.SetActive(true);
            EventSystem.current.firstSelectedGameObject = backMenuBt;
            EventSystem.current.SetSelectedGameObject(backMenuBt);
        }
    }
}
