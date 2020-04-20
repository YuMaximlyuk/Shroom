using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private Rigidbody2D m_PlayerRigidbody;
    private NewPlayerController m_PlayerController;
    [SerializeField]
    private Transform m_Platform;
    [SerializeField]
    private float m_PushForce;
    [SerializeField]
    private bool m_CanPush = false;
    [SerializeField]
    private float m_UpDistance;
    [SerializeField]
    private float m_DownDistance;
    [SerializeField]
    private float m_Velocity;

    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip m_CrashAudio;

    void Start()
    {
        m_Platform = transform.parent;

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerRigidbody = m_Player.GetComponent<Rigidbody2D>();
        m_PlayerController = m_Player.GetComponent<NewPlayerController>();

        m_PushForce = m_PlayerController.GetMinVelocity();

        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        //if (m_Player.transform.position.y - m_Platform.transform.position.y > m_Distance || m_Player.transform.position.y - m_Platform.transform.position.y < m_Distance)
        //{
        //    m_CanPush = true;
        //}
        //else
        //{
        //    m_CanPush = false;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (Mathf.Abs(Mathf.Abs(m_PlayerController.GetVelocity()) - m_PlayerController.GetMaxVelocity()) < 0.001f)
            //{
            //    Destroy(m_Platform.gameObject);
            //}
            //else
            //{
                Debug.Log("PushPlayer");
                PushPlayer();
            //}
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float bound = m_PlayerController.GetBoundY();

        if (m_Platform.transform.position.y - m_Player.transform.position.y < m_DownDistance && m_Player.transform.position.y - m_Platform.transform.position.y < m_UpDistance && Mathf.Abs(bound - m_Player.transform.position.y) > 0.001f)
        {
            m_Velocity = m_PlayerController.GetVelocity();
            Debug.Log("Stay");
            m_PlayerRigidbody.velocity = new Vector2(0, m_Velocity);
        }
        else
        {
            PushPlayer();
        }
    }

    private void PushPlayer()
    {
        //Debug.Log("Player velocity: " + m_PlayerController.GetVelocity());
        m_Velocity = m_PlayerController.GetVelocity();
        if (m_Velocity > 0 && m_Platform.transform.position.y - m_Player.transform.position.y > m_DownDistance)
        {
            Debug.Log("Down");
            m_PlayerRigidbody.velocity = new Vector2(0, -m_Velocity);
            //m_PlayerRigidbody.AddForce(Vector2.down * -m_Velocity, ForceMode2D.Impulse);
            Sound();
        }
        else if(m_Velocity < 0 && m_Player.transform.position.y - m_Platform.transform.position.y > m_UpDistance)
        {
            Debug.Log("Up");
            if (Mathf.Abs(Mathf.Abs(m_PlayerController.GetVelocity()) - m_PlayerController.GetMaxVelocity()) < 0.001f)
            {
                m_AudioSource.clip = m_CrashAudio;
                Sound();
                Destroy(m_Platform.gameObject);
            }
            m_PlayerRigidbody.velocity = new Vector2(0, m_PushForce);
            //m_PlayerRigidbody.AddForce(Vector2.up * m_PushForce, ForceMode2D.Impulse);
            Sound();
            SetBoundY();
        }
        else
        {
            Debug.Log("Middle");
            m_PlayerRigidbody.velocity = new Vector2(0, m_Velocity);
        }
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

    public bool CanPush()
    {
        return m_CanPush;
    }
}
