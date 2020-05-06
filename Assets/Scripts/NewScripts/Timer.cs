using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float m_StartTime = 0f;
    [SerializeField]
    private float m_Timer = 0f;
    [SerializeField]
    private float m_StartPauseTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_StartTime = Time.time;
    }

    public void UpdateTimer()
    {
        m_Timer = Time.time - m_StartTime;
    }

    public void UpdateStartTime()
    {
        m_StartTime = Time.time;
    }

    public float GetTimer()
    {
        return m_Timer;
    }

    public void StopTimer()
    {
        m_StartPauseTime = Time.time;
    }

    public void Continue()
    {
        m_StartTime += Time.time - m_StartPauseTime;
    }
}
