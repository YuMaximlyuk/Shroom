using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_BlockPlatforms;
    [SerializeField]
    private List<BlockController> m_BlockControllers;
    [SerializeField]
    private GameObject m_DownPlatform;
    [SerializeField]
    private GameObject m_DownBlockPlatform;


    void Start()
    {
        m_BlockPlatforms = GameObject.FindGameObjectsWithTag("Block");
        foreach (var bp in m_BlockPlatforms)
        {
            m_BlockControllers.Add(bp.GetComponentInChildren<BlockController>());
        }
        m_DownBlockPlatform.SetActive(false);
    }

    public void Back()
    {
        foreach(var bc in m_BlockControllers)
        {
            bc.SetCanCrash(true);
        }
        m_DownBlockPlatform.SetActive(true);
        m_DownPlatform.SetActive(false);
    }
}
