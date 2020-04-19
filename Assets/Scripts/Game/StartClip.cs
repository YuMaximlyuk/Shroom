using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClip : MonoBehaviour
{
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_AudioSource.Play();
    }
}
