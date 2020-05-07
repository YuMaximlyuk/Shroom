using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject ControlImage;

    private void Start()
    {
        ControlImage.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Control()
    {
        ControlImage.SetActive(true);
    }

    public void ControlExit()
    {
        ControlImage.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    
}
