using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;  // �÷��̾� �̹��� ��������Ʈ
    [SerializeField] private float knockbackSpeed = 100f;
    private Vector2 knockbackDir;
    private bool isKnockback = false;
    private float knockbackTime = 0.05f; // �з����� �ð�
    private float knockbackTimer = 0f;
    public float moveSpeed = 5f;     // ĳ���� �̵� �ӵ�
    public float jumpForce = 10f;    // ���� ��
    public int maxJumpCount = 2;     // �ִ� ���� Ƚ��
    public int MaxHp = 6;            // �ִ� ��Ʈ����Ʈ
    public int CurrentHp;            // ���� ��Ʈ����Ʈ
    public Transform player;         // player��ü ��ġ
    public Vector2 resetPosition;    // ��ġ �ʱⰪ
    private float playerX = 1;

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
        if (isKnockback)
        {
            rb.velocity = knockbackDir * knockbackSpeed;

            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= knockbackTime)
            {
                isKnockback = false;
                knockbackTimer = 0f;
            }


        }

        //Debug.Log(Vector2.left.ToString() + " " + Vector2.right.ToString());

        // �¿� �̵�
        float moveX = Input.GetAxis("Horizontal");

        playerX = moveX;

        if (isGrounded)
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if(moveX < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        if(moveX > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
        
        // ��� ���ó��
        if(transform.position.y < -10)
        {
            isfallen();
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

        // ���߿��� �Ʒ� ȭ��ǥ �Է½� �ϰ�   
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded && !isKnockback)
        {
            rb.velocity = new Vector2(0 , jumpForce * 0.2f);
            rb.velocity = new Vector2(0 , -jumpForce * 2f);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isKnockback = false;
            updateIsGrounded(true);
            jumpCount = 0;
        }
        // ���� �浹�����ÿ� �����ϴ°�
        if (collision.gameObject.CompareTag("enemy"))
        {

            jumpCount = 2;

            Vector2 enemyPos = collision.transform.position;
            Vector2 playerPos = transform.position;

            // Enemy�� ��� �ʿ� �ִ��� Ȯ��
            float angleDeg = (enemyPos.x > playerPos.x) ? 165f : 15f;
            // �˹�
            float angleRad = angleDeg * Mathf.Deg2Rad;
            knockbackDir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

            isKnockback = true;
            
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            CurrentHp -= 1;
          
            if (CurrentHp <= 0)
            {
                isDead = true;
                
            }

            HpBarSlider.value = (float)CurrentHp / MaxHp;

        }

        // üũ����Ʈ ���˽� �� �÷��̾� ��ġ�� resetPosition�� ����
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            Vector2 checkptPos = collision.transform.position;
            resetPosition = checkptPos;
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
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0f;
            transform.position = new Vector2(resetPosition.x, resetPosition.y);
            CurrentHp = MaxHp;
            HpBarSlider.value = 1.0F;
            isKnockback = false;
            isDead = false;
        }
    }

    private void updateIsGrounded(bool flag)
    {
        isGrounded = flag;
        anim.SetBool("isGrounded", flag);
    }

    private void isfallen()
    {
        isDead = true;
        //Debug.Log("������");
    }
}
