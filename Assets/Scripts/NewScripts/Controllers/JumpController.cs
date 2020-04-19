using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private Rigidbody2D m_PlayerRigidbody;
    private NewPlayerController m_PlayerController;
    [SerializeField]
    private Transform m_Platform;
    [SerializeField]
    private float m_PushForce = 0.5f;
    [SerializeField]
    private bool m_CanPush = false;
    [SerializeField]
    private float m_Distance = 0.8f;

    private AudioSource m_AudioSource;

    void Start()
    {
        m_Platform = transform.parent;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerRigidbody = m_Player.GetComponent<Rigidbody2D>();
        m_PlayerController = m_Player.GetComponent<NewPlayerController>();

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
        if (m_PlayerController.GetVelocity() > 0)
        {
            m_CanPush = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && m_CanPush)
        {
            Debug.Log("Enter");
            m_CanPush = false;
            PushPlayer();
            Sound();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Stay");
            if (m_CanPush)
            {
                Debug.Log("Push in stay");
                m_CanPush = false;
                PushPlayer();
                Sound();
            }
            else
            {
                float m_Velocity = m_PlayerController.GetVelocity();
                m_PlayerRigidbody.velocity = new Vector2(0, m_Velocity);
            }
        }
    }

    private void PushPlayer()
    {
        //Debug.Log("Player velocity: " + m_PlayerController.GetVelocity());
        m_PushForce = Mathf.Abs(m_PlayerController.GetVelocity());
        m_PlayerRigidbody.velocity = new Vector2(0, m_PushForce);
        m_PlayerController.SetVelocity(m_PushForce);
        SetBoundY();
    }

    private void SetBoundY()
    {
        float bound;
        float y = m_Player.transform.position.y;
        float minHeight = m_PlayerController.GetMinHeight();
        float height = m_PlayerController.GetHeight();
        float minVelocity = m_PlayerController.GetMinVelocity();
        float velocityDifference = m_PlayerController.GetVelocityDifference();
        bound = y + minHeight + ((m_PushForce - minVelocity) / velocityDifference) * height;
        m_PlayerController.SetBoundY(bound);
    }

    private void Sound()
    {
        m_AudioSource.Play();
    }

}
