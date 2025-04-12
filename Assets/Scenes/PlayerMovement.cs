using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;  // �÷��̾� �̹��� ��������Ʈ
    public float moveSpeed = 5f;     // ĳ���� �̵� �ӵ�
    public float jumpForce = 10f;    // ���� ��
    public int maxJumpCount = 2;     // �ִ� ���� Ƚ��
    public int MaxHp = 6;            // �ִ� ��Ʈ����Ʈ
    public int CurrentHp;            // ���� ��Ʈ����Ʈ
    public Transform player;         // player��ü ��ġ
    public Vector2 resetPosition;    // ��ġ �ʱⰪ

    public Slider HpBarSlider; // HP�� ������Ʈ

    private Rigidbody2D rb;          // Rigidbody2D ������Ʈ
    private int jumpCount = 0;       // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ����ִ��� Ȯ��
    private bool isDead = false;     // HP�� 0�̰ų� y��ǥ�� -10���� ������ Ȯ��
    Animator anim;
    
    void Start()
    {
        // Rigidbody2D ��������
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHp = MaxHp;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        // �¿� �̵�
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if(moveX < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        if(moveX > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
        

        // ���� �Է� ó��
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            updateIsGrounded(true);
        }
        else if(jumpCount == maxJumpCount)
        {
            updateIsGrounded(false);
        }
        if (rb.velocity.x == 0)
            anim.SetBool("iswalking", false);
        else
            anim.SetBool("iswalking", true);
        CheckBoundary();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڰ� �浹 �� ���� Ƚ�� �ʱ�ȭ(���� �ʿ�)
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("enemy"))
        {
            updateIsGrounded(true);
            jumpCount = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            CurrentHp -= 1;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);

            if (CurrentHp <= 0)
            {
                isDead = true;
                
            }

            HpBarSlider.value = (float)CurrentHp / MaxHp;

        }

        // üũ����Ʈ ���˽� �� �÷��̾� ��ġ�� resetPosition�� ����
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            Vector2 currentPosition = transform.position;
            resetPosition = currentPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������ �� ���� ����(���� �ʿ�)
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("enemy"))
        {
            updateIsGrounded(false);
        }

    }

   

    private void CheckBoundary()
    {
        // player��ü�� ��ġ, HPȮ���� ������
        Vector2 currentPosition = transform.position;


        if (isDead)
        {
            transform.position = new Vector2(resetPosition.x, resetPosition.y);
            CurrentHp = MaxHp;
            HpBarSlider.value = 1.0F;
            isDead = false;
        }
    }

    private void updateIsGrounded(bool flag)
    {
        isGrounded = flag;
        anim.SetBool("isGrounded", flag);
    }
}
