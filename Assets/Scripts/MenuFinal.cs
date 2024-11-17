using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuFinal : MonoBehaviour
{
    public static MenuFinal Instance;
    public GameObject currentBt;

    public GameObject contenedorLista;
    public TMP_Text text;

    public List<TMP_Text> listaTexts = new List<TMP_Text>();

    private void Awake()
    {
        Instance = this;
        this.enabled = false;

        ///check current first time to add tmp_text to list
        if (this.enabled == true)
        {
            contenedorLista = EventSystem.current.firstSelectedGameObject.transform.parent.gameObject;
            foreach (TMP_Text childText in contenedorLista.GetComponentsInChildren<TMP_Text>())
            {
                listaTexts.Add(childText);

            }
        }
        
    }
   

    // Update is called once per frame
    void Update()
    {
        currentBt = EventSystem.current.currentSelectedGameObject.gameObject;
        ChangeTextColor();


    }

    /// <summary>
    /// check current obj to make list of tmp_texts
    /// </summary>
    public void CheckList()
    {
        contenedorLista = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        foreach (TMP_Text childText in contenedorLista.GetComponentsInChildren<TMP_Text>())
        {
            listaTexts.Add(childText);

        }
    }
    public void ChangeTextColor()
    {
        foreach (TMP_Text obj in listaTexts)
        {

            for (int i = 0; i < currentBt.GetComponentsInChildren<TMP_Text>().Length; i++)
            {
                if (obj == currentBt.GetComponentInChildren<TMP_Text>())
                {
                    Debug.Log(obj);

                    text = currentBt.GetComponentInChildren<TMP_Text>();
                    text.color = Color.white;
                }
                else if (obj != currentBt.GetComponentInChildren<TMP_Text>())
                {

                    //text = currentBt.GetComponentInChildren<TMP_Text>();

                    obj.color = Color.black;
                }
            }
        }
    }
}
