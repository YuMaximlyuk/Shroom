using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_BlockPlatforms;
    [SerializeField]
    private GameObject m_DownPlatform;
    [SerializeField]
    private GameObject m_DownBlockPlatform;

    void Start()
    {
        m_BlockPlatforms = GameObject.FindGameObjectsWithTag("Block");
        m_DownBlockPlatform.SetActive(false);
    }

    public void Down()
    {
        for (int i = 0; i < m_BlockPlatforms.Length; ++i)
        {
            m_BlockPlatforms[i].GetComponentInChildren<BlockController>().SetCanCrash(true);
        }
        m_DownBlockPlatform.SetActive(true);
        m_DownPlatform.SetActive(false);

    }

    public void Up()
    {
        for (int i = 0; i < m_BlockPlatforms.Length; ++i)
        {
            m_BlockPlatforms[i].GetComponentInChildren<BlockController>().ResetBlock();
        }
        m_DownBlockPlatform.SetActive(false);
        m_DownPlatform.SetActive(true);
    }
}
