using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveraController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_Distance;
    [SerializeField]
    private GameObject m_Player;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");    
    }

    void Update()
    {
        if(transform.position.y - m_Player.GetComponent<Transform>().position.y > m_Distance)
        {
            transform.Translate(Vector2.down * m_Speed * Time.deltaTime);
        }
        else if(m_Player.GetComponent<Transform>().position.y - transform.position.y > m_Distance)
        {
            transform.Translate(Vector2.up * m_Speed * Time.deltaTime);
        }
    }
}
