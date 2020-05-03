using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1.0f;
    [SerializeField]
    private bool m_MoveRight = true;
    [SerializeField]
    private float m_Distance = 1.0f;
    //[SerializeField]
    private float m_RightPositionX;
    //[SerializeField]
    private float m_LeftPositionX;
    
    void Start()
    {
        m_RightPositionX = transform.position.x;
        if(m_MoveRight)
        {
            m_LeftPositionX = m_RightPositionX;
            m_RightPositionX += m_Distance;
        }
        else
        {
            m_LeftPositionX = m_RightPositionX - m_Distance;
        }
    }

    void Update()
    {
        if(m_MoveRight)
        {
            //Move right
            transform.Translate(Vector2.right * m_Speed * Time.deltaTime);
            if(m_RightPositionX - transform.position.x < 0.01f)
            {
                m_MoveRight = false;
            }
        }
        else
        {
            //Move left
            transform.Translate(Vector2.left * m_Speed * Time.deltaTime);
            if(transform.position.x - m_LeftPositionX < 0.01f)
            {
                m_MoveRight = true;
            }
        }
    }
}
