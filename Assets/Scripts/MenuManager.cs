using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject selectPanel;
    public GameObject quitPanel;
    public Button button;
    public GameObject currentBt;

    //first selected buttons
    public GameObject startBt;
    public GameObject lvl1Bt;
    public GameObject yesBt;

    public TMP_Text text;
    public List<TMP_Text> listaTexts = new List<TMP_Text>();

    public GameObject contenedorLista;

   


    private void Awake()
    {
       
        contenedorLista = EventSystem.current.firstSelectedGameObject.transform.parent.gameObject;
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
        quitPanel.SetActive(false);
        foreach (TMP_Text childText in contenedorLista.GetComponentsInChildren<TMP_Text>())
        {
            listaTexts.Add(childText);

        }

    }

    // Update is called once per frame
    void Update()
    {
        currentBt = EventSystem.current.currentSelectedGameObject.gameObject;
        ChangeTextColor();
    }
    public void ChangeScene(string scename)
    {
        SceneManager.LoadScene(scename);
    }
    public void ChangePanel()
    {
        mainPanel.SetActive(false);
        selectPanel.SetActive(true);
        //EventSystem.current.firstSelectedGameObject = lvl1Bt;
        EventSystem.current.SetSelectedGameObject(lvl1Bt);
        DeleteList();
        CheckList();
       

    }
    public void ChangeQuitPanel()
    {
        mainPanel.SetActive(false);
        selectPanel.SetActive(false);
        quitPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(yesBt);
        DeleteList();
        CheckList();


    }
    public void CloseQuitPanel()
    {
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
        quitPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(startBt);
        DeleteList();
        CheckList();



    }
    public void Exit()
    {
        Application.Quit();
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
    public void DeleteList()
    {
        listaTexts.Clear();

    }
    /// <summary>
    /// change color of texts
    /// </summary>
    public void ChangeTextColor()
    {

        foreach (TMP_Text obj in listaTexts)
        {

            for (int i = 0; i < currentBt.GetComponentsInChildren<TMP_Text>().Length; i++)
            {
                /// gets current object to change color text to white
                if (obj == currentBt.GetComponentInChildren<TMP_Text>())
                {
                    Debug.Log(obj);

                    text = currentBt.GetComponentInChildren<TMP_Text>();
                    text.color = Color.white;
                }
                /// cahnge color text black if not current
                else if (obj != currentBt.GetComponentInChildren<TMP_Text>())
                {


                    obj.color = Color.black;
                }
            }
        }
    }
}
