using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private NewPlayerController m_PlayerController;
    [SerializeField]
    private Transform m_Platform;
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    [SerializeField]
    private CapsuleCollider2D m_Collider;
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
    private AudioClip m_PolyporeAudio;
    [SerializeField]
    private AudioClip m_CrashAudio;


    [SerializeField]
    private bool m_CanCrash = false;
    [SerializeField]
    private Vector2 m_StartPosition;

    void Start()
    {
        m_Platform = transform.parent;
        m_StartPosition = new Vector2(m_Platform.transform.position.x, m_Platform.transform.position.y);
        m_Rigidbody = m_Platform.GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<CapsuleCollider2D>();

        m_Player = GameObject.FindGameObjectWithTag("Player");
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
            Debug.Log("PushPlayer");
            PushPlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float bound = m_PlayerController.GetBoundY();

            if (m_Platform.transform.position.y - m_Player.transform.position.y < m_DownDistance * m_Platform.transform.localScale.y && 
                m_Player.transform.position.y - m_Platform.transform.position.y < m_UpDistance * m_Platform.transform.localScale.y && 
                Mathf.Abs(bound - m_Player.transform.position.y) > 0.001f)
            {
                m_Velocity = m_PlayerController.GetVelocity();
                Debug.Log("Stay");
                m_PlayerController.SetVelocity(m_Velocity);
            }
            else
            {
                PushPlayer();
            }
        }
    }

    private void PushPlayer()
    {
        m_Velocity = m_PlayerController.GetVelocity();
        if (m_Velocity > 0 && m_Platform.transform.position.y - m_Player.transform.position.y > m_DownDistance * m_Platform.transform.localScale.y)
        {
            Debug.Log("Down");
            m_PlayerController.SetVelocity(-m_Velocity);
            Sound();
        }
        else if (m_Velocity < 0 && m_Player.transform.position.y - m_Platform.transform.position.y > m_UpDistance * m_Platform.transform.localScale.y)
        {
            Debug.Log("Up");
            if (Mathf.Abs(Mathf.Abs(m_PlayerController.GetVelocity()) - m_PlayerController.GetMaxVelocity()) < 0.001f && m_CanCrash)
            {
                m_AudioSource.clip = m_CrashAudio;
                Sound();
                m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                m_Collider.enabled = false;
                StartCoroutine(DeactivatePlatform());

            }
            else
            {
                Sound();
            }
            m_PlayerController.SetVelocity(m_PushForce);
            //Sound();
            SetBoundY();
        }
        else
        {
            Debug.Log("Middle");
            m_PlayerController.SetVelocity(m_Velocity);
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

    private IEnumerator DeactivatePlatform()
    {
        yield return new WaitForSeconds(1.8f);
        Debug.Log("Deactivate");
        m_Platform.gameObject.SetActive(false);
    }

    public void SetCanCrash(bool canCrash)
    {
        m_CanCrash = canCrash;
    }

    public void ResetBlock()
    {
        m_Platform.transform.position = m_StartPosition;
        m_CanCrash = false;
        m_AudioSource.clip = m_PolyporeAudio;
        m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        m_Collider.enabled = true;
        m_Platform.gameObject.SetActive(true);
    }
}
