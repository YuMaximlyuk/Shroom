using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MinVelocity = 0.5f;
    [SerializeField]
    private float m_MaxVelocity = 2.5f;
    private float m_MiddleVelocity;

    [SerializeField]
    private float m_MinHeight = 0.7f;
    [SerializeField]
    private float m_MaxHeight = 2.5f;
    private float m_Height;

    [SerializeField]
    private float m_Sensitivity = 2f;
    [SerializeField]
    private float m_Velocity;
    [SerializeField]
    private float m_BoundY;

    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;

    private void Start()
    {
        m_BoundY = 1000;
        m_Height = m_MaxHeight - m_MinHeight;
        m_MiddleVelocity = (m_MinVelocity + m_MaxVelocity) / 2.0f;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        //m_Rigidbody.AddForce(Vector2.down * m_MinVelocity, ForceMode2D.Impulse);
        m_Rigidbody.velocity = new Vector2(0, -m_MinVelocity);
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (m_Rigidbody.velocity.y < 0 && Input.GetMouseButton(0))
        {
            Move();
        }
        Animation();
        if (m_Rigidbody.velocity.y != 0f)
        {
            m_Velocity = m_Rigidbody.velocity.y;
        }
        if (m_BoundY - transform.position.y < 0.001f)
        {
            Debug.Log("Bound");
            m_Rigidbody.velocity = new Vector2(0f, -Mathf.Abs(m_Velocity));
            m_Velocity = -Mathf.Abs(m_Velocity);
            m_BoundY = float.MaxValue;
            Debug.Log(m_BoundY - transform.position.y);
        }
        if (m_Rigidbody.velocity.y < m_MinVelocity && m_Rigidbody.velocity.y > -m_MinVelocity)
        {
            if (m_Rigidbody.velocity.y > 0)
            {
                m_Rigidbody.velocity = new Vector2(0, m_MinVelocity);
                m_BoundY = transform.position.y + m_MinHeight;
            }
            else
            {
                m_Rigidbody.velocity = new Vector2(0, -m_MinVelocity);
            }
        }
        if (m_Rigidbody.velocity.y > m_MaxVelocity && m_Rigidbody.velocity.y < -m_MaxVelocity)
        {
            m_Rigidbody.velocity = (m_Rigidbody.velocity.y > 0) ? new Vector2(0, m_MaxVelocity) : new Vector2(0, -m_MaxVelocity);
            if (m_Rigidbody.velocity.y > 0)
            {
                m_Rigidbody.velocity = new Vector2(0, m_MaxVelocity);
                m_BoundY = transform.position.y + m_MinHeight + m_Height;
            }
            else
            {
                m_Rigidbody.velocity = new Vector2(0, -m_MaxVelocity);
            }
        }
    }

    private void Animation()
    {
        if (m_Rigidbody.velocity.y > 0)
        {
            m_Animator.SetBool("FlyUp", true);
        }
        else
        {
            m_Animator.SetBool("FlyUp", false);
        }
    }

    private void Move()
    {
        if (Input.GetAxis("Mouse Y") > 0)
        {
            if (m_Rigidbody.velocity.y + m_Sensitivity * Time.deltaTime > -m_MinVelocity)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, -m_MinVelocity);
            }
            else
            {
                m_Rigidbody.AddForce(Vector2.up * m_Sensitivity * Time.deltaTime, ForceMode2D.Impulse);
            }

        }
        else if (Input.GetAxis("Mouse Y") < 0)
        {
            if (m_Rigidbody.velocity.y - m_Sensitivity * Time.deltaTime < -m_MaxVelocity)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, -m_MaxVelocity);
            }
            else
            {
                m_Rigidbody.AddForce(Vector2.down * m_Sensitivity * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Translate(Vector2.right * m_Sensitivity * Time.deltaTime);
            if (m_SpriteRenderer.flipX)
            {
                m_SpriteRenderer.flipX = false;
            }
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Translate(Vector2.left * m_Sensitivity * Time.deltaTime);
            if (!m_SpriteRenderer.flipX)
            {
                m_SpriteRenderer.flipX = true;
            }
        }
    }

    public float GetVelocity()
    {
        return m_Velocity;
    }
    public void SetVelocity(float velocity)
    {
        m_Velocity = velocity;
    }

    public float GetHeight()
    {
        return m_Height;
    }

    public float GetMinHeight()
    {
        return m_MinHeight;
    }

    public float GetMinVelocity()
    {
        return m_MinVelocity;
    }

    public float GetMaxVelocity()
    {
        return m_MaxVelocity;
    }

    public float GetVelocityDifference()
    {
        return m_MaxVelocity - m_MinVelocity;
    }

    public float GetBoundY()
    {
        return m_BoundY;
    }

    public void SetBoundY(float bound)
    {
        m_BoundY = bound;
    }
}
