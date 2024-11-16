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
    public TMP_Text startText;


    


    private void Awake()
    {
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(string scename)
    {
        SceneManager.LoadScene(scename);
    }
    public void ChangePanel()
    {
        mainPanel.SetActive(false);
        selectPanel.SetActive(true);

    }
    public void ChangeQuitPanel()
    {
        mainPanel.SetActive(false);
        selectPanel.SetActive(false);
        quitPanel.SetActive(true);

    }
    public void CloseQuitPanel()
    {
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
        quitPanel.SetActive(false);

    }
    public void Exit()
    {
        Application.Quit();
    }
}
