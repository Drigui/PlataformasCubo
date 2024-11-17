using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject backMenuBt;
    public GameObject canvas;
    private void Awake()
    {
            canvas = transform.GetChild(0).gameObject;

    }
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //load current scene +1
        if (LevelManager.Instance.hasKey && SceneManager.GetActiveScene().name != GameConstant.LEVEL3_KEY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Entra");

        }
        else if (LevelManager.Instance.hasKey && SceneManager.GetActiveScene().name == GameConstant.LEVEL3_KEY)
        {
            LevelManager.Instance.winPanel.SetActive(true);
            EventSystem.current.firstSelectedGameObject = backMenuBt;
            EventSystem.current.SetSelectedGameObject(backMenuBt);
            
            ///activate menu final script, pick button obj and checklist
            MenuFinal.Instance.enabled = true;
            MenuFinal.Instance.contenedorLista = EventSystem.current.firstSelectedGameObject.transform.parent.gameObject;
            MenuFinal.Instance.CheckList();

        }
        else
        {
            if (collision.gameObject.CompareTag(GameConstant.PLAYER))
            {
                canvas.SetActive(true);

            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYER))
        {
            canvas.SetActive(false);
        }
    }
}
