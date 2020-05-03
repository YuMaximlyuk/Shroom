using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    //Канвас NPC. Включается, когда идет диалог с ним.
    [SerializeField]
    private Canvas m_Canvas;
    [SerializeField]
    private Image m_TextPanel;
    [SerializeField]
    private Text m_PhraseText;
    //Фразы NPC
    [SerializeField]
    [TextArea(2, 5)]
    private string[] m_Phrases;
    private Queue<string> m_PhrasesQueue;
    //Текущая фраза
    private string m_Phrase;
    //Дла определения пишется ли в данный момент фраза
    [SerializeField]
    private bool m_Writing = false;
    //Для определения октивен ли диалог
    [SerializeField]
    private bool m_Active = false;

    private NewPlayerController m_PlayerController;
    [SerializeField]
    private float m_PlayerVelocity;

    [SerializeField]
    private PlatformManager m_PlatformManager;

    void Start()
    {
        m_TextPanel.color = new Color(m_TextPanel.color.r, m_TextPanel.color.g, m_TextPanel.color.b, 0f);
        m_PhraseText.text = "";
        m_Canvas.gameObject.SetActive(false);
        m_PhrasesQueue = new Queue<string>();
        foreach(var phrase in m_Phrases)
        {
            m_PhrasesQueue.Enqueue(phrase);
        }

        var platformManager = GameObject.FindGameObjectWithTag("PlatformManager");
        m_PlatformManager = platformManager.GetComponent<PlatformManager>();
    }

    void Update()
    {
        if(m_Active)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(m_Writing)
                {
                    StopAllCoroutines();
                    m_Writing = false;
                    m_PhraseText.text = m_Phrase;
                }
                else
                {
                    if(m_PhrasesQueue.Count != 0)
                    {
                        m_Phrase = m_PhrasesQueue.Dequeue();
                        StartCoroutine(WritePhrase(m_Phrase));
                    }
                    else
                    {
                        StartCoroutine(EndDialog());
                        m_PlayerController.SetVelocity(-m_PlayerVelocity);
                        m_PlayerController.SetCanMove(true);
                        GetComponent<BoxCollider2D>().enabled = false;
                        m_Active = false;
                    }
                }
            }
        }
    }

    //Начало разговора
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_PlayerController = collision.GetComponent<NewPlayerController>();
            m_PlayerVelocity = m_PlayerController.GetVelocity();
            m_Active = true;
            m_Canvas.gameObject.SetActive(true);
            StartCoroutine(StartDialog());
        }
    }

    //Появление панели с текстом
    private IEnumerator StartDialog()
    {
        while(m_TextPanel.color.a < 0.5f)
        {
            yield return null;
            m_TextPanel.color = new Color(m_TextPanel.color.r, m_TextPanel.color.g, m_TextPanel.color.b, m_TextPanel.color.a + 0.05f);
        }
        m_Phrase = m_PhrasesQueue.Dequeue();
        StartCoroutine(WritePhrase(m_Phrase));
    }

    private IEnumerator EndDialog()
    {
        while (m_TextPanel.color.a > 0.001f)
        {
            yield return null;
            m_TextPanel.color = new Color(m_TextPanel.color.r, m_TextPanel.color.g, m_TextPanel.color.b, Mathf.Max(0f, m_TextPanel.color.a - 0.05f));
        }
        m_Canvas.gameObject.SetActive(false);
    }

    //Написание фразы
    private IEnumerator WritePhrase(string phrase)
    {
        m_Writing = true;
        m_PhraseText.text = "";
        foreach(var letter in phrase.ToCharArray())
        {
            m_PhraseText.text += letter;
            yield return null;
        }
        m_Writing = false;
    }
}
