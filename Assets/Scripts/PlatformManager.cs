using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockPlatforms;
    [SerializeField]
    private GameObject bottomPlatform;


    void Start()
    {
        blockPlatforms = GameObject.FindGameObjectsWithTag("Block");
        foreach(var bp in blockPlatforms)
        {
            bp.SetActive(false);
        }
    }
}
