using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_JumpForce;
    [SerializeField]
    private bool m_CanJump = true;
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * m_Speed * Time.deltaTime);
            m_SpriteRenderer.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * m_Speed * Time.deltaTime);
            m_SpriteRenderer.flipX = true;
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && m_CanJump)
        {
            StartCoroutine(JumpBoostAnim());
            JumpBoost();
        }
        if(m_Rigidbody.velocity.y > 0)
        {
            m_Animator.SetBool("FlyUp", true);
        }
        else
        {
            m_Animator.SetBool("FlyUp", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            m_CanJump = true;
        }
    }

    private void JumpBoost()
    {
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        m_CanJump = false;
    }

    private IEnumerator JumpBoostAnim()
    {
        m_Animator.SetBool("JumpBoost", true);
        yield return new WaitForSeconds(0.2f);
        m_Animator.SetBool("JumpBoost", false);
    }
}
