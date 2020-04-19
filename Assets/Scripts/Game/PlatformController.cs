using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private Rigidbody2D m_PlayerRigidbody;
    [SerializeField]
    private Transform m_Platform;
    [SerializeField]
    private float m_PushForce;
    [SerializeField]
    private bool m_CanPush = false;
    [SerializeField]
    private float m_Distance;

    [SerializeField]
    private AudioClip[] m_AudioClips;
    private AudioSource m_AudioSource;
    private PlatformController m_PlatformController;

    void Start()
    {
        m_Platform = transform.parent;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerRigidbody = m_Player.GetComponent<Rigidbody2D>();

        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_Player.transform.position.y - m_Platform.transform.position.y > m_Distance)
        {
            m_CanPush = true;
        }
        else
        {
            m_CanPush = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && m_CanPush)
        {
            PushPlayer();
            Sound();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && m_CanPush)
        {
            //PushPlayer();
        }
    }

    private void PushPlayer()
    {
        m_PlayerRigidbody.velocity = Vector2.zero;
        m_PlayerRigidbody.AddForce(Vector2.up * m_PushForce, ForceMode2D.Impulse);
    }

    private void Sound()
    {
        m_AudioSource.clip = m_AudioClips[Random.Range(0, m_AudioClips.Length)];
        m_AudioSource.Play();
    }

    public bool CanPush()
    {
        return m_CanPush;
    }
}
