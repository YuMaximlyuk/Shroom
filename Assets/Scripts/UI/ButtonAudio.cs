using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip m_OnButtonAudio;
    [SerializeField]
    private AudioClip m_OnClickButtonAudio;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void OnClickAudio()
    {
        m_AudioSource.clip = m_OnClickButtonAudio;
        m_AudioSource.Play();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_AudioSource.clip = m_OnButtonAudio;
        m_AudioSource.Play();
    }
}
