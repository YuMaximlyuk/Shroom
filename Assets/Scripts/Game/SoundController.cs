using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_AudioClips;
    private AudioSource m_AudioSource;
    private PlatformController m_PlatformController;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_PlatformController = GetComponent<PlatformController>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && m_AudioClips.Length > 0 && m_PlatformController.CanPush())
        {
            m_AudioSource.clip = m_AudioClips[Random.Range(0, m_AudioClips.Length)];
            m_AudioSource.Play();
        }
    }
}
