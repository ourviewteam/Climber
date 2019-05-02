using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControleUI : MonoBehaviour
{
    public BasicGamePlay BGP;
    public GameObject backgroundMusic;
    public GameObject TextBox;
    public GameObject objectsToHide;
    public GameObject WinState;
    public GameObject LooseState;

    private bool pouse = true;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("bmusic") == null)
        {
            Instantiate(backgroundMusic);
        }
        BGP = GameObject.FindGameObjectWithTag("AIM").GetComponent<BasicGamePlay>();
        BGP.PlayerShoted = true;
    }

    public void OnClose()
    {
        Application.Quit();
    }
    public void OnBeginGame()
    {
        pouse = false;
        TextBox.SetActive(false);
        objectsToHide.SetActive(false);
        BGP.PlayerShoted = false;
    }
    public void OnPouse()
    {
        pouse = !pouse;
        objectsToHide.SetActive(pouse);
        TextBox.SetActive(false);
    }
    public void CompletedLvl()
    {
        pouse = true;
        objectsToHide.SetActive(true);
        WinState.SetActive(true);
    }
    public void EndGame()
    {
        pouse = true;
        objectsToHide.SetActive(true);
        LooseState.SetActive(true);
    }

    public void OnReloadLvl()
    {
        SceneManager.LoadScene(0);
    }

}
