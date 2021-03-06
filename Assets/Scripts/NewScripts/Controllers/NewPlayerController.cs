﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MinVelocity = 1f;
    [SerializeField]
    private float m_MaxVelocity = 3f;
    private float m_MiddleVelocity;

    [SerializeField]
    private float m_MinHeight = 1f;
    [SerializeField]
    private float m_MaxHeight = 2.5f;
    private float m_Height;

    [SerializeField]
    private float m_Sensitivity = 1.5f;
    //[SerializeField]
    private float m_Velocity;
    [SerializeField]
    private float m_BoundY;

    [SerializeField]
    private GameObject m_DownPlatform;

    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;

    [SerializeField]
    private bool m_CanMove = true;

    private void Start()
    {
        m_BoundY = float.MaxValue;
        m_Height = m_MaxHeight - m_MinHeight;
        m_MiddleVelocity = (m_MinVelocity + m_MaxVelocity) / 2.0f;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.velocity = new Vector2(0, -m_MinVelocity);
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (m_CanMove)
        {
            if (m_Rigidbody.velocity.y < 0 && Input.GetMouseButton(0))
            {
                Speed();
                SideMove();

            }
            if (m_Rigidbody.velocity.y > 0 && Input.GetMouseButton(0))
            {
                SideMove();
            }
            Animation();
            if (m_Rigidbody.velocity.y != 0f)
            {
                m_Velocity = m_Rigidbody.velocity.y;
            }
            if (m_BoundY - transform.position.y < 0.001f)
            {
                //Debug.Log("Bound");
                m_Rigidbody.velocity = new Vector2(0f, -Mathf.Abs(m_Velocity));
                m_Velocity = -Mathf.Abs(m_Velocity);
                m_BoundY = float.MaxValue;
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

    private void Speed()
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
    }

    private void SideMove()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WormTrigger" || collision.gameObject.tag == "JellyfishTrigger")
        {
            Debug.Log("WormTrigger");
            m_CanMove = false;
            m_Rigidbody.velocity = Vector2.zero;
        }
        if(collision.gameObject.tag == "WormTrigger")
        {
            SetBoundY(m_DownPlatform.transform.position.y + m_MinHeight);
        }
        if(collision.gameObject.tag == "JellyfishTrigger")
        {
            var platformManager = GameObject.FindGameObjectWithTag("PlatformManager").GetComponent<PlatformManager>();
            platformManager.Back();
        }
    }

    public float GetVelocity()
    {
        return m_Velocity;
    }
    public void SetVelocity(float velocity)
    {
        m_Velocity = velocity;
        m_Rigidbody.velocity = new Vector2(0, velocity);
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

    public void SetCanMove(bool canMove)
    {
        m_CanMove = canMove;
    }
}
