using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PauseUI;
    private NewPlayerController m_PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        m_PauseUI.SetActive(false);
        m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_PauseUI.activeInHierarchy)
        {
            Debug.Log("Pause");
            m_PlayerController.Stop();
            m_PlayerController.GetComponent<Timer>().StopTimer();
            m_PauseUI.SetActive(true);
        }
    }

    public void Continue()
    {
        Debug.Log("Continue");
        m_PauseUI.SetActive(false);
        m_PlayerController.Continue();
        m_PlayerController.GetComponent<Timer>().Continue();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
